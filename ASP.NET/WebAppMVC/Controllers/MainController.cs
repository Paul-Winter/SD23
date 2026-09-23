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
            Response.ContentType = "text/html; charset=utf-8";

            string result = @"<form method='post'>
            <label>Login:</label><br/>
            <input name='login'/><br/>
            <label>Password:</label><br/>
            <input name='pass'/><br/>
            <input type='submit' value='SEND'/>";

            return new MainResult(result);
        }
        [HttpPost]
        public IActionResult Index(string login, string pass) // наименования параметров должны совпадать с полями name
        {
            return new MainResult($"<h2>Login: {login}</h2><h2>Password: {pass}</h2>");
        }

        //[HttpGet]
        public IActionResult Greet(string name)
        {
            return NotFound($"Not Found: {name}");
        }
        public IActionResult Meet()
        {
            return StatusCode(403);
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
