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
            return new MainResult($"<h3>Not Found: {name}</h3>");
        }
        public IActionResult Meet()
        {
            return new MainResult("<h2>403</h2>");
        }
        //[ActionName("Greeting")] - меняем имя действия
        //[HttpPost]
        //[NonAction]
        public IActionResult Hello()
        {
            return new MainResult("<h1>Hello, World!</h1>");
        }
        //[HttpDelete]
        [NonAction] // - отключаем действие (не все методы - действия)
        public string Greeting()
        {
            return "Hello, User!";
        }
    }
}
