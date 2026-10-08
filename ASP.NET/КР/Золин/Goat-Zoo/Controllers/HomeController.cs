using Microsoft.AspNetCore.Mvc;

namespace Goat_Zoo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
