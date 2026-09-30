using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            return View(await db.Persons.ToListAsync());
        }
        //public IActionResult Index(Person person)
        //{
        //    return View(person);
        //}
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            db.Persons.Add(person);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Helper()
        {
            return View();
        }
    }
}
