using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index(Person person)
        {
            return View(person);
        }
        //public string Index(Person person)
        //{
        //    return $"{person}";
        //}
    }
}
