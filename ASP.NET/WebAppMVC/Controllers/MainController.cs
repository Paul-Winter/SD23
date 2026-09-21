using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    //[NonController] - отключаем контроллер
    public class MainController : Controller
    {
        public string Index()
        {
            return "This is Index action!";
        }
        //[ActionName("Greeting")] - меняем имя действия
        public string Hello()
        {
            return "Hello, World!";
        }
        [NonAction] // - отключаем действие (не все методы - действия)
        public string Greeting()
        {
            return "Hello, User!";
        }
    }
}
