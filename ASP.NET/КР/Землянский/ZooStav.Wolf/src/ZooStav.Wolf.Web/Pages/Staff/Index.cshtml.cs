using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Pages.Staff;

/// <summary>
/// Сводка для работника: последняя кормёжка (когда, кто кормил, чем, сколько часов назад),
/// последние комментарии и журнал изменений — кто, когда и что менял.
/// </summary>
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly DiaryService _diary;
    private readonly AuditService _audit;
    private readonly CurrentUser _current;
    private readonly IConfiguration _config;

    public IndexModel(AppDbContext db, DiaryService diary, AuditService audit, CurrentUser current, IConfiguration config)
    {
        _db = db;
        _diary = diary;
        _audit = audit;
        _current = current;
        _config = config;
    }

    public StaffDashboardDto Dto { get; private set; } = new();
    public List<Animal> Animals { get; private set; } = new();
    public int SelectedAnimalId { get; private set; }
    public bool CanEdit => Permissions.CanEditDiary(_current.Role);

    [TempData] public string? Flash { get; set; }

    public async Task<IActionResult> OnGetAsync(int? animalId)
    {
        Animals = await _db.Animals.AsNoTracking().OrderBy(a => a.Id).ToListAsync();
        if (Animals.Count == 0) return Page();

        var id = animalId ?? Animals.FirstOrDefault(a => a.IsMainPage)?.Id ?? Animals[0].Id;
        SelectedAnimalId = id;
        Dto = await _diary.GetDashboardAsync(id, _config.GetValue("Zoo:FeedingAlertHours", 24));
        return Page();
    }

    /// <summary>Быстрая отметка кормёжки прямо со сводки: время и специалист фиксируются автоматически.</summary>
    public async Task<IActionResult> OnPostQuickFeedAsync(int animalId, string? foodType, decimal? foodAmountKg, string? details)
    {
        if (!CanEdit) return Forbid();
        var animal = await _db.Animals.FirstOrDefaultAsync(a => a.Id == animalId);
        if (animal == null) return NotFound();

        var entry = new DiaryEntry
        {
            AnimalId = animalId,
            Type = DiaryEntryType.Feeding,
            OccurredAt = DateTime.Now,
            Title = $"Кормёжка (быстрая отметка) — {animal.Name}",
            Details = string.IsNullOrWhiteSpace(details) ? "Кормёжка отмечена со сводки кабинета." : details!,
            FoodType = string.IsNullOrWhiteSpace(foodType) ? "Стандартный рацион" : foodType,
            FoodAmountKg = foodAmountKg,
            IsPublic = true,
            AuthorId = _current.Id!,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.DiaryEntries.Add(entry);
        await _db.SaveChangesAsync();

        await _audit.LogAsync("DiaryEntry", entry.Id, "Created",
            $"Кормёжка особи «{animal.Name}»: {entry.FoodType}, {entry.FoodAmountKg?.ToString("0.##") ?? "—"} кг (специалист: {_current.DisplayName})");

        Flash = $"Кормёжка зафиксирована: {DateTime.Now:HH:mm}, специалист — {_current.DisplayName}.";
        return RedirectToPage("/Staff/Index", new { animalId });
    }
}
