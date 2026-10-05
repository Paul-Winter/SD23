using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StavZoo.Data;

namespace StavZoo.Pages.Lion;

public class IndexModel : PageModel
{
    private readonly ZooDbContext db;

    public IndexModel(ZooDbContext db)
    {
        this.db = db;
    }

    public Models.Lion? Animal { get; set; }
    public int TotalDonated { get; set; }
    public List<Models.Donation> DonationList { get; set; } = new();

    public int EditDonationId { get; set; }
    public int EditDonationAmount { get; set; }

    public void OnGet(string animalName, int? editDonationId)
    {
        Animal = db.Lions.FirstOrDefault(l => l.Slug.ToLower() == animalName.ToLower());

        if (Animal != null)
        {
            DonationList = db.Donations
                .Where(d => d.LionId == Animal.Id)
                .OrderByDescending(d => d.DonatedAt)
                .ToList();

            TotalDonated = DonationList.Sum(d => d.Amount);

            if (editDonationId != null)
            {
                var d = db.Donations.FirstOrDefault(x => x.Id == editDonationId);
                if (d != null)
                {
                    EditDonationId = d.Id;
                    EditDonationAmount = d.Amount;
                }
            }
        }
    }

    public IActionResult OnPostDonate(int amount, string animalName)
    {
        var lion = db.Lions.FirstOrDefault(l => l.Slug == animalName);
        if (lion == null) return NotFound();

        var donation = new Models.Donation
        {
            LionId = lion.Id,
            Amount = amount,
            DonatedAt = DateTime.UtcNow
        };

        db.Donations.Add(donation);
        db.SaveChanges();

        TempData["Message"] = $"Спасибо! Ваш донат {amount} ₽ принят.";
        return Redirect($"/stav-zoo/{animalName}");
    }

    public IActionResult OnPostEditDonation(int id, int amount, string animalName)
    {
        var donation = db.Donations.FirstOrDefault(d => d.Id == id);
        if (donation == null) return NotFound();

        donation.Amount = amount;
        db.SaveChanges();

        return Redirect($"/stav-zoo/{animalName}");
    }

    public IActionResult OnPostDeleteDonation(int id, string animalName)
    {
        var donation = db.Donations.FirstOrDefault(d => d.Id == id);
        if (donation != null)
        {
            db.Donations.Remove(donation);
            db.SaveChanges();
        }

        return Redirect($"/stav-zoo/{animalName}");
    }

    public IActionResult OnPostClearDonations(string animalName)
    {
        var lion = db.Lions.FirstOrDefault(l => l.Slug == animalName);
        if (lion == null) return NotFound();

        var donations = db.Donations.Where(d => d.LionId == lion.Id).ToList();
        db.Donations.RemoveRange(donations);
        db.SaveChanges();

        return Redirect($"/stav-zoo/{animalName}");
    }

    public IActionResult OnPostUploadPhoto(string animalName, IFormFile photo)
    {
        var lion = db.Lions.FirstOrDefault(l => l.Slug == animalName);
        if (lion == null || photo == null || photo.Length == 0) return NotFound();

        var folder = Path.Combine("wwwroot", "images", "lion");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
        var fullPath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            photo.CopyTo(stream);
        }

        lion.PhotoUrl = "/images/lion/" + fileName;
        db.SaveChanges();

        return Redirect($"/stav-zoo/{animalName}");
    }
}