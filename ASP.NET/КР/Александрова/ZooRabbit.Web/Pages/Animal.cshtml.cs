using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooRabbit.Web.Data;
using ZooRabbit.Web.Models;

namespace ZooRabbit.Web.Pages;

public class AnimalModel : PageModel
{
    private readonly ZooDbContext _db;
    private readonly IWebHostEnvironment _env;
    public AnimalModel(ZooDbContext db, IWebHostEnvironment env) { _db = db; _env = env; }

    public Animal? Animal { get; private set; }
    public List<DiaryEntry> Diary { get; private set; } = new();
    public int TotalDonations { get; private set; }
    public bool IsEmployee => User.IsInRole("Employee");
    [TempData] public string? Message { get; set; }

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        Animal = await _db.Animals.AsNoTracking().FirstOrDefaultAsync(a => a.Slug == slug);
        if (Animal is null) { Response.StatusCode = 404; return Page(); }
        TotalDonations = await _db.Donations.Where(d => d.AnimalId == Animal.Id).SumAsync(d => d.Amount);
        if (IsEmployee)
            Diary = await _db.DiaryEntries.AsNoTracking().Where(e => e.AnimalId == Animal.Id)
                .OrderByDescending(e => e.Date).ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostEditAsync(string slug, string? name, string? species, string? description,
        string? videoUrl, string? webcamUrl, IFormFile? photo)
    {
        if (!IsEmployee) return StatusCode(403);
        var a = await _db.Animals.FirstOrDefaultAsync(x => x.Slug == slug);
        if (a is null) return NotFound();
        if (!string.IsNullOrWhiteSpace(name)) a.Name = name.Trim();
        a.Species = species ?? "";
        a.Description = description ?? "";
        a.VideoUrl = string.IsNullOrWhiteSpace(videoUrl) ? null : videoUrl.Trim();
        a.WebcamUrl = string.IsNullOrWhiteSpace(webcamUrl) ? null : webcamUrl.Trim();
        if (photo is { Length: > 0 })
        {
            var ext = Path.GetExtension(photo.FileName).ToLowerInvariant();
            if (ext is ".jpg" or ".jpeg" or ".png" or ".webp")
            {
                var dir = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(dir);
                using (var fs = System.IO.File.Create(Path.Combine(dir, slug + ext)))
                    await photo.CopyToAsync(fs);
                a.PhotoUrl = $"/images/{slug}{ext}?v={DateTime.UtcNow.Ticks}";
            }
        }
        await _db.SaveChangesAsync();
        Message = "Сохранено";
        return RedirectToPage(new { slug });
    }

    public async Task<IActionResult> OnPostAddDiaryAsync(string slug, DiaryEntryType type, string? note)
    {
        if (!IsEmployee) return StatusCode(403);
        var a = await _db.Animals.FirstOrDefaultAsync(x => x.Slug == slug);
        if (a is null) return NotFound();
        if (!string.IsNullOrWhiteSpace(note))
        {
            _db.DiaryEntries.Add(new DiaryEntry { AnimalId = a.Id, Type = type, Note = note.Trim(), Author = User.Identity!.Name! });
            await _db.SaveChangesAsync();
        }
        return RedirectToPage(new { slug });
    }

    public async Task<IActionResult> OnPostDeleteDiaryAsync(string slug, int id)
    {
        if (!IsEmployee) return StatusCode(403);
        var e = await _db.DiaryEntries.FirstOrDefaultAsync(x => x.Id == id);
        if (e != null) { _db.DiaryEntries.Remove(e); await _db.SaveChangesAsync(); }
        return RedirectToPage(new { slug });
    }

    public async Task<IActionResult> OnPostDonateAsync(string slug, int amount, string? purpose, string? donorName)
    {
        var a = await _db.Animals.FirstOrDefaultAsync(x => x.Slug == slug);
        if (a is null) return NotFound();
        if (amount > 0)
        {
            _db.Donations.Add(new Donation { AnimalId = a.Id, Amount = amount, Purpose = purpose ?? "Корм",
                DonorName = string.IsNullOrWhiteSpace(donorName) ? "Аноним" : donorName });
            await _db.SaveChangesAsync();
            Message = "Спасибо за помощь!";
        }
        return RedirectToPage(new { slug });
    }
}
