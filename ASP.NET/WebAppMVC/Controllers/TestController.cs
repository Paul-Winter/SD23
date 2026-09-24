using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "It TOP College";
            ViewBag.Message = "работает тестовый контроллер";
            return View();
        }
    }
}
