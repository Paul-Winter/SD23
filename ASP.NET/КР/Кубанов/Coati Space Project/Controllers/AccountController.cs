using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Coati_Space_Project.Models;
using System.Security.Claims;

namespace Coati_Space_Project.Controllers
{
    public class AccountController : Controller
    {
        ApplicationContext db;

        public AccountController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Diary");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Пожалуйста, введите логин и пароль сотрудника.";
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            var staffUser = await db.StaffUsers.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (staffUser == null)
            {
                ViewBag.Error = "Неверный логин или пароль. Попробуйте еще раз.";
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, staffUser.FullName),
                new Claim(ClaimTypes.NameIdentifier, staffUser.Username),
                new Claim(ClaimTypes.Role, staffUser.Role),
                new Claim("Position", staffUser.Position)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Diary");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Coati", "Zoo");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
