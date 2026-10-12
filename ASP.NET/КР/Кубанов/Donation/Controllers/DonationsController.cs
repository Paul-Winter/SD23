using Donation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Donation.Controllers
{
    public class DonationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
