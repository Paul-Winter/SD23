using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
