using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Pages.Staff.Entry;

/// <summary>Создание записи дневника: кормёжка, вакцинация, спаривание, потомство, болезнь и пр.</summary>
public class NewModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly AuditService _audit;
    private readonly CurrentUser _current;

    public NewModel(AppDbContext db, AuditService audit, CurrentUser current)
    {
        _db = db;
        _audit = audit;
        _current = current;
    }

    [BindProperty]
    public DiaryEntryInput Input { get; set; } = new();

    public List<Animal> Animals { get; private set; } = new();

    [TempData] public string? Flash { get; set; }

    public async Task<IActionResult> OnGetAsync(int? animalId, DiaryEntryType? type)
    {
        if (!Permissions.CanEditDiary(_current.Role)) return Forbid();

        Animals = await _db.Animals.AsNoTracking().OrderBy(a => a.Id).ToListAsync();
        Input.AnimalId = animalId ?? Animals.FirstOrDefault(a => a.IsMainPage)?.Id ?? Animals.FirstOrDefault()?.Id ?? 0;
        if (type.HasValue) Input.Type = type.Value;

        ViewData["Animals"] = Animals;
        ViewData["Title"] = "Новая запись дневника";
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!Permissions.CanEditDiary(_current.Role)) return Forbid();

        Animals = await _db.Animals.AsNoTracking().OrderBy(a => a.Id).ToListAsync();
        ViewData["Animals"] = Animals;
        ViewData["Title"] = "Новая запись дневника";

        if (!Animals.Any(a => a.Id == Input.AnimalId))
        {
            ModelState.AddModelError("Input.AnimalId", "Выберите особь");
        }

        if (!ModelState.IsValid) return Page();

        var entry = Input.ToEntity(_current.Id!);
        _db.DiaryEntries.Add(entry);
        await _db.SaveChangesAsync();

        var animalName = Animals.First(a => a.Id == entry.AnimalId).Name;
        await _audit.LogAsync("DiaryEntry", entry.Id, "Created",
            $"Создана запись «{entry.Title}» ({entry.Type.ToRussian()}) для «{animalName}» на {entry.OccurredAt:dd.MM.yyyy HH:mm}. " +
            $"Специалист: {_current.DisplayName} ({_current.Position})");

        Flash = $"Запись «{entry.Title}» добавлена. Автор: {_current.DisplayName}, время внесения: {DateTime.Now:dd.MM.yyyy HH:mm}.";
        return RedirectToPage("/Staff/Diary", new { animalId = entry.AnimalId });
    }
}
