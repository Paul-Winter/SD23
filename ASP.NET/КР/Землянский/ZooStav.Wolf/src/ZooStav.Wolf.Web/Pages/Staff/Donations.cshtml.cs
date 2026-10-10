using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Pages.Staff;

/// <summary>Донаты: сводка и список с фильтрами; отметка об обработке.</summary>
public class DonationsModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly DonationService _donations;
    private readonly AuditService _audit;
    private readonly CurrentUser _current;

    public DonationsModel(AppDbContext db, DonationService donations, AuditService audit, CurrentUser current)
    {
        _db = db;
        _donations = donations;
        _audit = audit;
        _current = current;
    }

    public DonationsPageViewModel Vm { get; private set; } = new();

    [TempData] public string? Flash { get; set; }

    public async Task OnGetAsync(string? purpose, DateTime? from, DateTime? to, bool onlyUnprocessed = false, int page = 1)
    {
        await LoadAsync(purpose, from, to, onlyUnprocessed, page);
    }

    public async Task<IActionResult> OnPostMarkProcessedAsync(int id)
    {
        var donation = await _db.Donations.FirstOrDefaultAsync(d => d.Id == id);
        if (donation == null) return NotFound();

        donation.IsProcessed = true;
        await _db.SaveChangesAsync();
        await _audit.LogAsync("Donation", donation.Id, "Updated",
            $"Донат №{donation.Id} ({donation.Amount:N0} ₽, {donation.Purpose}) отмечен как обработанный. Специалист: {_current.DisplayName}");

        Flash = $"Донат №{id} отмечен как обработанный ({_current.DisplayName}, {DateTime.Now:dd.MM.yyyy HH:mm}).";
        return RedirectToPage("/Staff/Donations");
    }

    private async Task LoadAsync(string? purpose, DateTime? from, DateTime? to, bool onlyUnprocessed, int page)
    {
        var q = _db.Donations.AsNoTracking().AsQueryable();
        if (!string.IsNullOrEmpty(purpose)) q = q.Where(d => d.Purpose == purpose);
        if (from.HasValue) q = q.Where(d => d.CreatedAt >= from.Value.Date.ToUniversalTime());
        if (to.HasValue) q = q.Where(d => d.CreatedAt < to.Value.Date.AddDays(1).ToUniversalTime());
        if (onlyUnprocessed) q = q.Where(d => !d.IsProcessed);

        const int pageSize = 20;
        if (page < 1) page = 1;
        var total = await q.CountAsync();

        Vm = new DonationsPageViewModel
        {
            Page = new PagedResult<Donation>
            {
                Items = await q.OrderByDescending(d => d.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            },
            Summary = await _donations.GetSummaryAsync(),
            Purpose = purpose,
            From = from,
            To = to,
            OnlyUnprocessed = onlyUnprocessed
        };

        ViewData["Title"] = "Донаты";
    }
}
