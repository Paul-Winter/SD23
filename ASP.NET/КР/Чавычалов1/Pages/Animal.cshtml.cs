using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace StavZoo.Pages;

public class AnimalModel(ZooDb db) : PageModel
{
    public Animal Animal { get; private set; } = null!;
    public decimal Total { get; private set; }
    public decimal Food { get; private set; }
    public decimal Treatment { get; private set; }
    public List<DiaryEntry> Diary { get; private set; } = new();

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        var a = await db.Animals.FirstOrDefaultAsync(x => x.Slug == slug);
        if (a is null) return NotFound();
        Animal = a;

        var don = await db.Donations.Where(d => d.AnimalId == a.Id)
            .Select(d => new { d.Amount, d.Purpose }).ToListAsync();
        Total = don.Sum(x => x.Amount);
        Food = don.Where(x => x.Purpose == DonationPurpose.Food).Sum(x => x.Amount);
        Treatment = don.Where(x => x.Purpose == DonationPurpose.Treatment).Sum(x => x.Amount);

        if (User.IsInRole("Keeper") || User.IsInRole("Admin"))
            Diary = await db.Diary.Where(d => d.AnimalId == a.Id)
                .OrderByDescending(d => d.OccurredAt).Take(100).ToListAsync();
        return Page();
    }
}
