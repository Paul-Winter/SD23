using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class FormController : Controller
    {
        string marriage = "";
        List<User> users = new List<User>
        {
            new User(12345, "John_Doe", "qwerty", "doe.john@umail.com", new DateOnly(2002, 02, 20), "male", "yes", "C#"),
            new User(23456, "Jack_Black", "12345", "black.j@umail.com", new DateOnly(2003, 03, 30), "male","no", "Python"),
            new User(43215, "JW", "asdf", "walker.johny@umail.com", new DateOnly(2010, 10, 20), "male","no", "Rust")
        };
        List<Depart> departs = new List<Depart>
        {
            new Departs(54321, )
        };


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
        public IActionResult People()
        {
            return View(users);
        }
    }
}
