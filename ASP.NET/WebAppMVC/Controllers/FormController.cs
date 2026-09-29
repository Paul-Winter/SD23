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
        static List<Depart> departs = new List<Depart>
        {
            new Depart(54321, "TestingOtdel", "street.Bogdavov", "+7 785-394-192", "testing@mail.ru"),
            new Depart(92643, "DevelopOtdel", "street.Pushkina", "+7 909-906-240", "develop@mail.ru"),
            new Depart(92570, "RPO-Otdel", "street.Kulakova", "+7 420-185-934", "rpo@mail.com")
        };
        List<Employee> employees = new List<Employee>
        {
            new Employee(3518, "Vova Vist", new DateOnly(2001, 12, 23), departs[1]),
            new Employee(7412, "Dima Kurnosov", new DateOnly(1999, 06, 13), departs[0]),
            new Employee(6882, "Oleg Ruslanovich", new DateOnly(2003, 02, 28), departs[2]),
            new Employee(1287, "Arseniy Zorkin", new DateOnly(2003, 09, 06), departs[2]),
            new Employee(7384, "Artem Vadimovich", new DateOnly(1998, 02, 11), departs[1]),
            new Employee(5394, "Oleg Mongol", new DateOnly(2004, 04, 18), departs[0]),
            new Employee(2983, "Denis Denisovich", new DateOnly(2002, 08, 26), departs[1])
        };

        public IActionResult Staff(int? departId)
        {
            List<Depart> companyDeparts = departs.Select(d => new Depart(d.Id, d.Name, d.Adress, d.Phone, d.Email)).ToList();
            CompanyViewModel viewModel = new()
            {
                Departs = companyDeparts,
                Employees = employees
            };
            if (departId != null && departId >= 0)
            {
                viewModel.Employees = employees.Where(e => e.Depart.Id == departId);
            }
            return View(viewModel);
        }
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
