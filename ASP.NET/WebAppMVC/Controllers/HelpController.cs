using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class HelpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
        //[ActionName("KeepCalm")]
        public IActionResult Test()
        {
            return View("KeepCalm");
        }
    }
}
