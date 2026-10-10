using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;

namespace ZooStav.Wolf.Web.Pages.Staff;

/// <summary>
/// Вход для работников зоопарка (cookie-аутентификация для веб-интерфейса).
/// API использует JWT: POST /api/auth/login.
/// </summary>
public class LoginModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly PasswordService _passwords;
    private readonly CookieAuthService _cookies;
    private readonly AuditService _audit;

    public LoginModel(AppDbContext db, PasswordService passwords, CookieAuthService cookies, AuditService audit)
    {
        _db = db;
        _passwords = passwords;
        _cookies = cookies;
        _audit = audit;
    }

    [BindProperty]
    public LoginViewModel Input { get; set; } = new();

    public void OnGet(string? returnUrl = null)
    {
        Input.ReturnUrl = returnUrl;
        ViewData["Title"] = "Вход для работников";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ViewData["Title"] = "Вход для работников";

        if (!ModelState.IsValid)
        {
            Input.Error = "Заполните логин и пароль.";
            return Page();
        }

        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == Input.UserName);
        if (user == null || !user.IsActive || !_passwords.Verify(user, Input.Password))
        {
            Input.Error = "Неверный логин или пароль.";
            return Page();
        }

        user.LastLoginAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        await _cookies.SignInAsync(HttpContext, user);

        // Фиксируем вход: кто, когда, с какого IP.
        await _audit.LogAsAsync(user.Id, user.DisplayName, user.Role,
            HttpContext.Connection.RemoteIpAddress?.ToString(),
            "AppUser", user.Id, "Login",
            $"Вход в кабинет работника: {user.DisplayName} ({Models.Roles.ToRussian(user.Role)}, {user.Position})");

        if (!string.IsNullOrEmpty(Input.ReturnUrl) && Url.IsLocalUrl(Input.ReturnUrl))
            return Redirect(Input.ReturnUrl);

        return RedirectToPage("/Staff/Index");
    }
}
