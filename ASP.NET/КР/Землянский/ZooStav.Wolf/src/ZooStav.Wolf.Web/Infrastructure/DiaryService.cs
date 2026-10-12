using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Infrastructure;

public class DiaryFilter
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public DiaryEntryType? Type { get; set; }
    public string? AuthorId { get; set; }
    public int? AnimalId { get; set; }
    public string? Search { get; set; }
    public bool IncludeNonPublic { get; set; }
    public string Sort { get; set; } = "date_desc";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public DiaryFilter Normalized() => new()
    {
        From = From, To = To, Type = Type, AuthorId = AuthorId, AnimalId = AnimalId,
        Search = string.IsNullOrWhiteSpace(Search) ? null : Search.Trim(),
        IncludeNonPublic = IncludeNonPublic,
        Sort = Sort == "date_asc" ? "date_asc" : "date_desc",
        Page = Page < 1 ? 1 : Page,
        PageSize = PageSize is < 1 or > 200 ? 10 : PageSize
    };
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalCount { get; set; }
    public int TotalPages => PageSize <= 0 ? 1 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrev => Page > 1;
    public bool HasNext => Page < TotalPages;
}

public class FeedingStatus
{
    public DiaryEntry? LastFeeding { get; set; }
    public string LastFeedingSpecialist { get; set; } = "—";
    public TimeSpan? TimeSinceLastFeeding { get; set; }
    public bool IsOverdue { get; set; }
    public int FeedingsLast24h { get; set; }
    public int FeedingsLast7Days { get; set; }
    public double? LastFoodAmountKg { get; set; }
    public string LastFoodType { get; set; } = "—";
    public string NextPlannedFeeding { get; set; } = "—";
}

public class StaffDashboardDto
{
    public Animal? Animal { get; set; }
    public FeedingStatus Feeding { get; set; } = new();
    public int TotalEntries { get; set; }
    public int NonPublicEntries { get; set; }
    public int OpenIllnesses { get; set; }
    public int VaccinationsThisYear { get; set; }
    public int CommentsLast7Days { get; set; }
    public List<DiaryEntry> RecentEntries { get; set; } = new();
    public List<DiaryComment> RecentComments { get; set; } = new();
    public List<AuditLog> RecentChanges { get; set; } = new();
    public Dictionary<DiaryEntryType, int> CountByType { get; set; } = new();
    public decimal DonationsFood { get; set; }
    public decimal DonationsTreatment { get; set; }
}

public class DiaryService
{
    private readonly AppDbContext _db;

    public DiaryService(AppDbContext db) => _db = db;

    public IQueryable<DiaryEntry> Query(DiaryFilter f, bool canSeeNonPublic)
    {
        f = f.Normalized();
        var q = _db.DiaryEntries
            .Include(e => e.Author)
            .Include(e => e.Animal)
            .Include(e => e.Comments)
            .AsQueryable();

        if (!canSeeNonPublic) q = q.Where(e => e.IsPublic);
        if (f.IncludeNonPublic && canSeeNonPublic) {  }

        if (f.From.HasValue) q = q.Where(e => e.OccurredAt >= f.From.Value.Date);
        if (f.To.HasValue) q = q.Where(e => e.OccurredAt < f.To.Value.Date.AddDays(1));
        if (f.Type.HasValue) q = q.Where(e => e.Type == f.Type.Value);
        if (!string.IsNullOrEmpty(f.AuthorId)) q = q.Where(e => e.AuthorId == f.AuthorId);
        if (f.AnimalId.HasValue) q = q.Where(e => e.AnimalId == f.AnimalId.Value);
        if (!string.IsNullOrEmpty(f.Search))
        {
            var s = f.Search;
            q = q.Where(e => EF.Functions.Like(e.Title, $"%{s}%")
                          || EF.Functions.Like(e.Details, $"%{s}%")
                          || EF.Functions.Like(e.FoodType ?? "", $"%{s}%")
                          || EF.Functions.Like(e.Medication ?? "", $"%{s}%")
                          || EF.Functions.Like(e.Diagnosis ?? "", $"%{s}%"));
        }

        q = f.Sort == "date_asc"
            ? q.OrderBy(e => e.OccurredAt).ThenBy(e => e.Id)
            : q.OrderByDescending(e => e.OccurredAt).ThenByDescending(e => e.Id);

        return q;
    }

    public async Task<PagedResult<DiaryEntry>> GetPagedAsync(DiaryFilter f, bool canSeeNonPublic)
    {
        f = f.Normalized();
        var q = Query(f, canSeeNonPublic);
        var total = await q.CountAsync();
        var items = await q.Skip((f.Page - 1) * f.PageSize).Take(f.PageSize).ToListAsync();
        return new PagedResult<DiaryEntry> { Items = items, Page = f.Page, PageSize = f.PageSize, TotalCount = total };
    }

    public async Task<FeedingStatus> GetFeedingStatusAsync(int animalId, int alertHours = 24)
    {
        var last = await _db.DiaryEntries
            .Include(e => e.Author)
            .Where(e => e.AnimalId == animalId && e.Type == DiaryEntryType.Feeding)
            .OrderByDescending(e => e.OccurredAt)
            .FirstOrDefaultAsync();

        var since = DateTime.Now.AddDays(-1);
        var week = DateTime.Now.AddDays(-7);

        var status = new FeedingStatus
        {
            LastFeeding = last,
            LastFeedingSpecialist = last?.Author?.DisplayName ?? "—",
            LastFoodType = last?.FoodType ?? "—",
            LastFoodAmountKg = (double?)last?.FoodAmountKg,
            FeedingsLast24h = await _db.DiaryEntries.CountAsync(e => e.AnimalId == animalId && e.Type == DiaryEntryType.Feeding && e.OccurredAt >= since),
            FeedingsLast7Days = await _db.DiaryEntries.CountAsync(e => e.AnimalId == animalId && e.Type == DiaryEntryType.Feeding && e.OccurredAt >= week)
        };

        if (last != null)
        {
            status.TimeSinceLastFeeding = DateTime.Now - last.OccurredAt;
            status.IsOverdue = status.TimeSinceLastFeeding.Value.TotalHours > alertHours;
        }

        var plannedHour = last?.OccurredAt.Hour ?? 9;
        status.NextPlannedFeeding = DateTime.Today.AddDays(1).AddHours(plannedHour).ToString("dd.MM HH:mm");
        return status;
    }

    public async Task<StaffDashboardDto> GetDashboardAsync(int animalId, int alertHours = 24)
    {
        var animal = await _db.Animals.FirstOrDefaultAsync(a => a.Id == animalId);
        var yearStart = new DateTime(DateTime.Now.Year, 1, 1);
        var week = DateTime.Now.AddDays(-7);

        var dto = new StaffDashboardDto
        {
            Animal = animal,
            Feeding = await GetFeedingStatusAsync(animalId, alertHours),
            TotalEntries = await _db.DiaryEntries.CountAsync(e => e.AnimalId == animalId),
            NonPublicEntries = await _db.DiaryEntries.CountAsync(e => e.AnimalId == animalId && !e.IsPublic),
            OpenIllnesses = await _db.DiaryEntries.CountAsync(e => e.AnimalId == animalId && e.Type == DiaryEntryType.Illness),
            VaccinationsThisYear = await _db.DiaryEntries.CountAsync(e => e.AnimalId == animalId && e.Type == DiaryEntryType.Vaccination && e.OccurredAt >= yearStart),
            CommentsLast7Days = await _db.DiaryComments.CountAsync(c => c.DiaryEntry!.AnimalId == animalId && c.CreatedAt >= week),
            RecentEntries = await _db.DiaryEntries.Include(e => e.Author)
                .Where(e => e.AnimalId == animalId).OrderByDescending(e => e.OccurredAt).Take(6).ToListAsync(),
            RecentComments = await _db.DiaryComments.Include(c => c.Author).Include(c => c.DiaryEntry)
                .Where(c => c.DiaryEntry!.AnimalId == animalId).OrderByDescending(c => c.CreatedAt).Take(6).ToListAsync(),
            RecentChanges = await _db.AuditLogs.OrderByDescending(a => a.TimestampUtc).Take(8).ToListAsync()
        };

        var donationRows = await _db.Donations.AsNoTracking().Select(d => new { d.Purpose, d.Amount }).ToListAsync();
        dto.DonationsFood = donationRows.Where(d => d.Purpose is DonationPurposes.Food or DonationPurposes.Free).Sum(d => d.Amount);
        dto.DonationsTreatment = donationRows.Where(d => d.Purpose == DonationPurposes.Treatment).Sum(d => d.Amount);

        dto.CountByType = await _db.DiaryEntries.Where(e => e.AnimalId == animalId)
            .GroupBy(e => e.Type)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Type, x => x.Count);

        return dto;
    }
}
