using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebAppMVC.Controllers
{
    //[NonController] - отключаем контроллер
    public class MainController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string login, string pass) // наименования параметров должны совпадать с полями name
        {
            return new MainResult($"<h2>Login: {login}</h2><h2>Password: {pass}</h2>");
        }

        //[HttpGet]
        public IActionResult Greet(string name)
        {
            return View();
        }
        public IActionResult Meet()
        {
            return View("~/Views/Home/Test.cshtml");
        }
        //[ActionName("Greeting")] - меняем имя действия
        //[HttpPost]
        //[NonAction]
        public IActionResult Hello()
        {
            return View("Greet");
        }
        //[HttpDelete]
        [NonAction] // - отключаем действие (не все методы - действия)
        public string Greeting()
        {
            return "Hello, User!";
        }
    }
}
