using Microsoft.AspNetCore.Mvc;

namespace Goat_Zoo.Controllers
{
    public class NotesController : Controller
    {
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }
    }
}
