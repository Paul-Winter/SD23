using Microsoft.AspNetCore.Mvc;

namespace Goat_Zoo.Controllers
{
    public class MainPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
