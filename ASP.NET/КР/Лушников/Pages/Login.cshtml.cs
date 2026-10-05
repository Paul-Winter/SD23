using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StavZoo.Data;
using StavZoo.Services;

namespace StavZoo.Pages;

public class LoginModel : PageModel
{
    private readonly ZooDbContext db;
    private readonly JwtService jwt;

    public LoginModel(ZooDbContext db, JwtService jwt)
    {
        this.db = db;
        this.jwt = jwt;
    }

    public string? Error { get; set; }

    public void OnGet() { }

    public IActionResult OnPost(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Error = "Заполните все поля";
            return Page();
        }

        var user = db.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            Error = "Неверное имя пользователя или пароль";
            return Page();
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            Error = "Неверное имя пользователя или пароль";
            return Page();
        }

        var token = jwt.GenerateToken(user.Username, user.Role);

        Response.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddHours(8)
        });

        return RedirectToPage("/Index");
    }
}