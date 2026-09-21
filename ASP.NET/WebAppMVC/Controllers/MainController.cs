using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    //[NonController] - отключаем контроллер
    public class MainController : Controller
    {
        //[HttpGet]
        public string Index()
        {
            return "Hello, World!";
        }
        //[HttpGet]
        public string Greet(string name)
        {
            return $"Hello, {name}!";
        }
        //[ActionName("Greeting")] - меняем имя действия
        //[HttpPost]
        [NonAction]
        public string Hello()
        {
            return "Hello, World!";
        }
        //[HttpDelete]
        [NonAction] // - отключаем действие (не все методы - действия)
        public string Greeting()
        {
            return "Hello, User!";
        }
    }
}
