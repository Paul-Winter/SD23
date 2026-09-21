using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebAppMVC.Controllers
{
    //[NonController] - отключаем контроллер
    public class MainController : Controller
    {
        //[HttpGet]
        public async Task Index()
        {
            Response.ContentType = "text/html; charset=utf-8";
            StringBuilder result = new StringBuilder("<h2>Request Headers</h2><table>");
            foreach(var header in Request.Headers)
            {
                result.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
            }
            result.Append("</table>");
            await Response.WriteAsync(result.ToString());
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
