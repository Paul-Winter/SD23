using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StavZoo.Pages;

public class LogoutModel : PageModel
{
    public IActionResult OnGet() => Redirect("/");
    public IActionResult OnPost()
    {
        Response.Cookies.Delete("zoo_jwt");
        return Redirect("/");
    }
}
