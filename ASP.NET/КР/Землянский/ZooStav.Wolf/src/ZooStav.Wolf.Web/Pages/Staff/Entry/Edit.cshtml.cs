using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Pages.Staff.Entry;

/// <summary>
/// Редактирование записи. Кипер и ветеринар могут править только свои записи, администратор — любые.
/// Каждое изменение фиксируется в журнале: кто, когда и какие поля изменились.
/// </summary>
public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly AuditService _audit;
    private readonly CurrentUser _current;

    public EditModel(AppDbContext db, AuditService audit, CurrentUser current)
    {
        _db = db;
        _audit = audit;
        _current = current;
    }

    [BindProperty]
    public DiaryEntryInput Input { get; set; } = new();

    public DiaryEntry Entry { get; private set; } = new();
    public List<Animal> Animals { get; private set; } = new();
    public List<AuditLog> History { get; private set; } = new();

    [TempData] public string? Flash { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (!await LoadAsync(id)) return NotFound();
        Input = DiaryEntryInput.From(Entry);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!await LoadAsync(id)) return NotFound();

        if (!ModelState.IsValid) return Page();

        var before = new DiaryEntry
        {
            Type = Entry.Type, OccurredAt = Entry.OccurredAt, Title = Entry.Title, Details = Entry.Details,
            FoodType = Entry.FoodType, FoodAmountKg = Entry.FoodAmountKg, Medication = Entry.Medication,
            Dosage = Entry.Dosage, Diagnosis = Entry.Diagnosis, WeightKg = Entry.WeightKg, IsPublic = Entry.IsPublic
        };

        Input.ApplyTo(Entry);
        Entry.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var diff = AuditService.Diff(before, Entry);
        await _audit.LogAsync("DiaryEntry", Entry.Id, "Updated",
            $"Изменена запись №{Entry.Id} «{Entry.Title}». {diff}. Специалист: {_current.DisplayName} ({_current.Position})");

        Flash = $"Изменения сохранены: {_current.DisplayName}, {DateTime.Now:dd.MM.yyyy HH:mm}.";
        return RedirectToPage("/Staff/Entry/Edit", new { id });
    }

    private async Task<bool> LoadAsync(int id)
    {
        if (!Permissions.CanEditDiary(_current.Role)) return false;

        var entry = await _db.DiaryEntries.Include(e => e.Author).Include(e => e.Animal)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (entry == null) return false;

        // Кипер/ветеринар правит только собственные записи, администратор — любые.
        if (_current.Role != Roles.Admin && entry.AuthorId != _current.Id) return false;

        Entry = entry;
        Animals = await _db.Animals.AsNoTracking().OrderBy(a => a.Id).ToListAsync();
        History = await _db.AuditLogs.AsNoTracking()
            .Where(a => a.EntityName == "DiaryEntry" && a.EntityId == id.ToString())
            .OrderByDescending(a => a.TimestampUtc).ToListAsync();

        ViewData["Animals"] = Animals;
        ViewData["Title"] = $"Изменение записи №{id}";
        return true;
    }
}
