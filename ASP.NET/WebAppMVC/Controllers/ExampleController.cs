using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class ExampleController : Controller
    {
        ApplicationContext db;
        public ExampleController(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Index(Person person)
        {
            return View(person);
        }
        //public string Index(Person person)
        //{
        //    return $"{person}";
        //}
        public IActionResult Helper()
        {
            return View();
        }
    }
}
