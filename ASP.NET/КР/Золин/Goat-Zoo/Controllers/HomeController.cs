using Goat_Zoo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Goat_Zoo.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Webcam()
        {
            return View();
        }

        public IActionResult Donate()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
