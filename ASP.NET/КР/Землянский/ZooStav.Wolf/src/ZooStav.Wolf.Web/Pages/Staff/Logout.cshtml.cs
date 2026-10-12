using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZooStav.Wolf.Web.Infrastructure;

namespace ZooStav.Wolf.Web.Pages.Staff;

public class LogoutModel : PageModel
{
    private readonly CookieAuthService _cookies;
    private readonly AuditService _audit;
    private readonly CurrentUser _current;

    public LogoutModel(CookieAuthService cookies, AuditService audit, CurrentUser current)
    {
        _cookies = cookies;
        _audit = audit;
        _current = current;
    }

    /// <summary>Выход только POST-запросом (защита от CSRF-подобных переходов по ссылке).</summary>
    public async Task<IActionResult> OnPostAsync()
    {
        if (_current.IsAuthenticated)
        {
            await _audit.LogAsync("AppUser", _current.Id ?? "", "Logout",
                $"Выход из кабинета: {_current.DisplayName} ({_current.RoleRussian})");
            await _cookies.SignOutAsync(HttpContext);
        }

        return RedirectToPage("/Staff/Logout");
    }

    public IActionResult OnGet() => RedirectToPage("/Staff/Index");
}
