using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZooRabbit.Web.Data;
using ZooRabbit.Web.Services;

namespace ZooRabbit.Web.Pages;

public class LoginModel : PageModel
{
    private readonly ZooDbContext _db;
    private readonly IConfiguration _cfg;
    public LoginModel(ZooDbContext db, IConfiguration cfg) { _db = db; _cfg = cfg; }

    [BindProperty] public string Username { get; set; } = "";
    [BindProperty] public string Password { get; set; } = "";
    public string? Error { get; private set; }

    public IActionResult OnPost()
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == Username);
        if (user is null || !Passwords.Verify(user, Password))
        {
            Error = "Неверный логин или пароль";
            return Page();
        }
        Response.Cookies.Append("jwt", TokenService.Create(user, _cfg), new CookieOptions
        {
            HttpOnly = true, SameSite = SameSiteMode.Lax, Expires = DateTimeOffset.UtcNow.AddHours(8)
        });
        return Redirect("/");
    }
}
