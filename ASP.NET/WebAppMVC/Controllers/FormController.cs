using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class FormController : Controller
    {
        string marriage = "";
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string Index(string login, string pass, DateOnly birth, string email,
                            string gender, bool isMarried, string progLang)
        {
            if (isMarried)
            {
                marriage = "женат / замужем";
            }
            else
            {
                marriage = "холост / не замужем";
            }
            return $"User login:           {login}\n" +
                   $"Password:             {pass}\n" +
                   $"E-mail:               {email}\n" +
                   $"Birth Date:           {birth}\n" +
                   $"Gender:               {gender}\n" +
                   $"Marriage:             {marriage}\n" +
                   $"Programming language: {progLang}\n";
        }
    }
}
