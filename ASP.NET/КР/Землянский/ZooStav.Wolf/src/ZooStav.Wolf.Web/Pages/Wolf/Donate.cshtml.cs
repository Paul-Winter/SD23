using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Pages.Wolf;

/// <summary>Донаты на корм и лечение особи (включая free donation любой суммы).</summary>
public class DonateModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly DonationService _donations;
    private readonly AuditService _audit;

    public DonateModel(AppDbContext db, DonationService donations, AuditService audit)
    {
        _db = db;
        _donations = donations;
        _audit = audit;
    }

    [BindProperty]
    public DonationViewModel Input { get; set; } = new();

    public Animal Animal { get; private set; } = new();
    public DonationSummary Summary { get; private set; } = new();
    public List<Donation> Recent { get; private set; } = new();
    public Donation? LastDonation { get; private set; }

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        var loaded = await LoadAsync(slug);
        if (!loaded) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string slug)
    {
        var loaded = await LoadAsync(slug);
        if (!loaded) return NotFound();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var donation = await _donations.AddAsync(new Donation
        {
            DonorName = string.IsNullOrWhiteSpace(Input.DonorName) ? "Анонимный даритель" : Input.DonorName!.Trim(),
            Email = Input.Email,
            Amount = Input.Amount,
            Purpose = DonationPurposes.All.Contains(Input.Purpose) ? Input.Purpose : DonationPurposes.Free,
            IsRecurring = Input.IsRecurring,
            Comment = Input.Comment,
            AnimalId = Animal.Id,
            Provider = "demo",
            IsProcessed = false
        });

        var purposeText = donation.Amount > 0 ? $"{donation.Amount:N0} ₽ на «{donation.Purpose}»" : "free donation (без суммы)";
        await _audit.LogAsAsync("guest", donation.DonorName, "Guest", HttpContext.Connection.RemoteIpAddress?.ToString(),
            "Donation", donation.Id, "DonationCreated",
            $"Новый донат {purposeText} от {donation.DonorName} для {Animal.Name}");

        LastDonation = donation;
        Summary = await _donations.GetSummaryAsync();
        Recent = await _db.Donations.AsNoTracking().Where(d => d.Amount > 0)
            .OrderByDescending(d => d.CreatedAt).Take(8).ToListAsync();

        ViewData["Title"] = $"Донат — {Animal.Name}";
        ViewData["AnimalSlug"] = Animal.SubdomainSlug;
        ViewData["AnimalName"] = Animal.Name;
        return Page();
    }

    private async Task<bool> LoadAsync(string slug)
    {
        var animal = await _db.Animals.AsNoTracking().FirstOrDefaultAsync(a => a.SubdomainSlug == slug);
        if (animal == null) return false;

        Animal = animal;
        Summary = await _donations.GetSummaryAsync();
        Recent = await _db.Donations.AsNoTracking().Where(d => d.Amount > 0)
            .OrderByDescending(d => d.CreatedAt).Take(8).ToListAsync();

        ViewData["Title"] = $"Донат для {animal.Name}";
        ViewData["AnimalSlug"] = animal.SubdomainSlug;
        ViewData["AnimalName"] = animal.Name;
        return true;
    }
}
