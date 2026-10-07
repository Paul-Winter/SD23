using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZooRabbit.Web.Pages;

public class LogoutModel : PageModel
{
    public IActionResult OnGet() => Redirect("/");
    public IActionResult OnPost()
    {
        Response.Cookies.Delete("jwt");
        return Redirect("/");
    }
}
