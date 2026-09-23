using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebAppMVC.Controllers
{
    //[NonController] - отключаем контроллер
    public class MainController : Controller
    {
        [HttpGet]
        public async Task Index()
        {
            Response.ContentType = "text/html; charset=utf-8";

            string result = @"<form method='post'>
            <label>Login:</label><br/>
            <input name='login'/><br/>
            <label>Password:</label><br/>
            <input name='pass'/><br/>
            <input type='submit' value='SEND'/>";

            await Response.WriteAsync(result);
        }
        [HttpPost]
        public string Index(string login, string pass)
        {
            return $"Login: {login}\nPassword: {pass}";
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
