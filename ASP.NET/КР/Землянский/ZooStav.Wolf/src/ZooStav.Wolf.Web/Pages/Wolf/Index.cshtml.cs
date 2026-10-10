using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Pages.Wolf;

/// <summary>
/// Страница животного. Доступна как по адресу поддомена (wolf.zoostav.ru → /wolf/wolf),
/// так и по «резервному» пути /wolf/{slug} на основном домене.
/// </summary>
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly DiaryService _diary;
    private readonly DonationService _donations;
    private readonly IConfiguration _config;

    public IndexModel(AppDbContext db, DiaryService diary, DonationService donations, IConfiguration config)
    {
        _db = db;
        _diary = diary;
        _donations = donations;
        _config = config;
    }

    public WolfPageViewModel Vm { get; private set; } = new();

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        var animal = await _db.Animals
            .Include(a => a.Partner)
            .Include(a => a.Media)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.SubdomainSlug == slug);

        if (animal == null) return NotFound();

        var alertHours = _config.GetValue("Zoo:FeedingAlertHours", 24);

        Vm = new WolfPageViewModel
        {
            Animal = animal,
            Photos = animal.Media.Where(m => m.Kind == MediaKinds.Photo && m.IsPublished).OrderByDescending(m => m.CreatedAt).ToList(),
            Videos = animal.Media.Where(m => m.Kind == MediaKinds.Video && m.IsPublished).OrderByDescending(m => m.CreatedAt).ToList(),
            Webcam = animal.Media.FirstOrDefault(m => m.Kind == MediaKinds.Webcam && m.IsPublished),
            PackMembers = await _db.Animals.AsNoTracking().Where(a => a.Id != animal.Id).OrderBy(a => a.Id).ToListAsync(),
            RecentPublicEntries = await _db.DiaryEntries.Include(e => e.Author).Include(e => e.Comments)
                .Where(e => e.AnimalId == animal.Id && e.IsPublic)
                .OrderByDescending(e => e.OccurredAt).Take(5).ToListAsync(),
            Feeding = await _diary.GetFeedingStatusAsync(animal.Id, alertHours),
            Donations = await _donations.GetSummaryAsync(),
            RequestedSubdomain = HttpContext.Items["ZooSubdomain"]?.ToString(),
            MainSiteUrl = _config["Zoo:MainSiteUrl"] ?? "https://zoostav.ru"
        };

        ViewData["Title"] = $"Северный волк {animal.Name}";
        ViewData["AnimalSlug"] = animal.SubdomainSlug;
        ViewData["AnimalName"] = animal.Name;
        return Page();
    }
}
