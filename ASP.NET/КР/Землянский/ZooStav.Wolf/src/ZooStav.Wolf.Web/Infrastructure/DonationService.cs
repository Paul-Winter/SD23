using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Infrastructure;

public class DonationSummary
{
    public decimal Total { get; set; }
    public decimal Food { get; set; }
    public decimal Treatment { get; set; }
    public decimal Enclosure { get; set; }
    public decimal Free { get; set; }
    public int Count { get; set; }
    public decimal GoalFood { get; set; } = 60_000m;
    public decimal GoalTreatment { get; set; } = 30_000m;
    public int PercentFood => GoalFood <= 0 ? 0 : (int)Math.Min(100, Math.Round(Food / GoalFood * 100));
    public int PercentTreatment => GoalTreatment <= 0 ? 0 : (int)Math.Min(100, Math.Round(Treatment / GoalTreatment * 100));
}

public class DonationService
{
    private readonly AppDbContext _db;

    public DonationService(AppDbContext db) => _db = db;

    public async Task<DonationSummary> GetSummaryAsync()
    {

        var rows = await _db.Donations.AsNoTracking()
            .Select(d => new { d.Purpose, d.Amount })
            .ToListAsync();

        decimal SumFor(string purpose) => rows.Where(r => r.Purpose == purpose).Sum(r => r.Amount);

        var s = new DonationSummary
        {
            Food = SumFor(DonationPurposes.Food),
            Treatment = SumFor(DonationPurposes.Treatment),
            Enclosure = SumFor(DonationPurposes.Enclosure),
            Free = SumFor(DonationPurposes.Free),
            Count = rows.Count
        };
        s.Total = s.Food + s.Treatment + s.Enclosure + s.Free;
        return s;
    }

    public async Task<Donation> AddAsync(Donation donation)
    {
        if (donation.Amount < 0) donation.Amount = 0;
        if (string.IsNullOrWhiteSpace(donation.DonorName)) donation.DonorName = "Анонимный даритель";
        donation.CreatedAt = DateTime.UtcNow;
        _db.Donations.Add(donation);
        await _db.SaveChangesAsync();
        return donation;
    }
}
