using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StavZoo.Data;

namespace StavZoo.Pages;

public class IndexModel : PageModel
{
    private readonly ZooDbContext db;

    public IndexModel(ZooDbContext db)
    {
        this.db = db;
    }

    public List<Models.Lion> Animals { get; set; } = new();
    public int TotalDonated { get; set; }

    public void OnGet()
    {
        Animals = db.Lions.ToList();
        TotalDonated = db.Donations.Sum(d => d.Amount);
    }
}