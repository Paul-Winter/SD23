using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Coati_Space_Project.Models;

namespace Coati_Space_Project.Controllers
{
    public class ZooController : Controller
    {
        ApplicationContext db;

        public ZooController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var coati = await db.Coatis.FirstOrDefaultAsync();
            return View(coati);
        }

        public async Task<IActionResult> Coati()
        {
            var coati = await db.Coatis.FirstOrDefaultAsync();
            if (coati == null)
            {
                coati = new Coati
                {
                    Name = "Хахатуха",
                    Gender = "Самец",
                    BirthDate = new DateOnly(2021, 5, 14),
                    Biography = "Хахатуха — всеобщий любимец зоопарка. Активный, любознательный и дружелюбный обитатель вольера.",
                    Diet = "Фрукты, перепелиные яйца, насекомые, нежирное мясо.",
                    Habitat = "Вольер №7, Сектор «Южная Америка».",
                    HealthStatus = "Клинически здоров",
                    PhotoUrl = "/images/coati-main.jpg",
                    VideoUrl = "/videos/coati-stream.mp4"
                };
            }

            var recentEntries = await db.DiaryEntries
                .OrderByDescending(e => e.EventDate)
                .Take(4)
                .ToListAsync();

            var totalDonations = await db.Donations.SumAsync(d => (decimal?)d.Amount) ?? 0;
            var donationsCount = await db.Donations.CountAsync();

            var imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            var galleryList = new List<string>();
            if (Directory.Exists(imagesDir))
            {
                galleryList = Directory.GetFiles(imagesDir, "coati-gallery-*.jpg")
                    .OrderBy(f => f)
                    .Select(f => "/images/" + Path.GetFileName(f))
                    .ToList();
            }

            if (!galleryList.Any())
            {
                galleryList = new List<string>
                {
                    "/images/coati-gallery-1.jpg",
                    "/images/coati-gallery-2.jpg",
                    "/images/coati-gallery-3.jpg",
                    "/images/coati-gallery-4.jpg",
                    "/images/coati-gallery-5.jpg",
                    "/images/coati-gallery-6.jpg",
                    "/images/coati-gallery-7.jpg",
                    "/images/coati-gallery-8.jpg"
                };
            }

            var viewModel = new CoatiViewModel
            {
                Coati = coati,
                GalleryImages = galleryList,
                RecentDiaryEntries = recentEntries,
                TotalDonations = totalDonations,
                DonationsCount = donationsCount
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Donate()
        {
            return Redirect("https://pay.cloudtips.ru/p/cceba1a9");
        }
    }
}
