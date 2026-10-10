using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Pages.Staff;

/// <summary>
/// Рабочий дневник: видны все записи, включая служебные.
/// Фильтры — по дате, типу, автору, особи, тексту; сортировка и постраничный вывод.
/// Здесь же добавляются комментарии специалистов и правятся записи.
/// </summary>
public class DiaryModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly DiaryService _diary;
    private readonly AuditService _audit;
    private readonly CurrentUser _current;

    public DiaryModel(AppDbContext db, DiaryService diary, AuditService audit, CurrentUser current)
    {
        _db = db;
        _diary = diary;
        _audit = audit;
        _current = current;
    }

    public DiaryPageViewModel Vm { get; private set; } = new();
    public bool CanEdit => Permissions.CanEditDiary(_current.Role);
    public string CurrentUserId => _current.Id ?? string.Empty;

    [TempData] public string? Flash { get; set; }

    public async Task<IActionResult> OnGetAsync(DateTime? from, DateTime? to, DiaryEntryType? type,
        string? authorId, int? animalId, string? search, bool includeNonPublic = false,
        string? sort = "date_desc", int page = 1)
    {
        await LoadAsync(from, to, type, authorId, animalId, search, includeNonPublic, sort, page);
        return Page();
    }

    /// <summary>Добавление комментария работника: фиксируются автор, должность и точное время.</summary>
    public async Task<IActionResult> OnPostCommentAsync(int entryId, string text)
    {
        if (!_current.IsEmployee) return Forbid();
        if (string.IsNullOrWhiteSpace(text))
        {
            Flash = "Комментарий не добавлен: пустой текст.";
            return RedirectToPage("/Staff/Diary");
        }

        var entry = await _db.DiaryEntries.FirstOrDefaultAsync(e => e.Id == entryId);
        if (entry == null) return NotFound();

        var comment = new DiaryComment
        {
            DiaryEntryId = entryId,
            Text = text.Trim(),
            AuthorId = _current.Id!,
            CreatedAt = DateTime.UtcNow
        };
        _db.DiaryComments.Add(comment);
        await _db.SaveChangesAsync();

        await _audit.LogAsync("DiaryComment", comment.Id, "CommentAdded",
            $"Комментарий к записи №{entryId} «{entry.Title}»: {(text.Length > 120 ? text[..120] + "…" : text)} " +
            $"(специалист: {_current.DisplayName}, {_current.Position})");

        Flash = $"Комментарий добавлен: {_current.DisplayName}, {DateTime.Now:dd.MM.yyyy HH:mm}.";
        return RedirectToPage("/Staff/Diary", new { entryId = (int?)null });
    }

    /// <summary>Удаление записи — только администратор; действие попадает в журнал изменений.</summary>
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        if (!Permissions.CanDeleteAnyEntry(_current.Role))
        {
            Flash = "Удаление записей доступно только администратору.";
            return RedirectToPage("/Staff/Diary");
        }

        var entry = await _db.DiaryEntries.FirstOrDefaultAsync(e => e.Id == id);
        if (entry == null) return NotFound();

        var title = entry.Title;
        var animalName = (await _db.Animals.FirstOrDefaultAsync(a => a.Id == entry.AnimalId))?.Name ?? "—";
        _db.DiaryEntries.Remove(entry);
        await _db.SaveChangesAsync();

        await _audit.LogAsync("DiaryEntry", id, "Deleted",
            $"Удалена запись «{title}» (№{id}, особь «{animalName}») — специалист: {_current.DisplayName}");
        Flash = $"Запись «{title}» удалена (специалист: {_current.DisplayName}, {DateTime.Now:dd.MM.yyyy HH:mm}).";
        return RedirectToPage("/Staff/Diary");
    }

    public async Task<IActionResult> OnPostDeleteCommentAsync(int id)
    {
        var comment = await _db.DiaryComments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null) return NotFound();
        if (!Permissions.CanModerateComments(_current.Role) && comment.AuthorId != _current.Id) return Forbid();

        _db.DiaryComments.Remove(comment);
        await _db.SaveChangesAsync();
        await _audit.LogAsync("DiaryComment", id, "CommentDeleted",
            $"Удалён комментарий №{id} к записи №{comment.DiaryEntryId} — специалист: {_current.DisplayName}");
        Flash = "Комментарий удалён, действие зафиксировано в журнале.";
        return RedirectToPage("/Staff/Diary");
    }

    private async Task LoadAsync(DateTime? from, DateTime? to, DiaryEntryType? type, string? authorId,
        int? animalId, string? search, bool includeNonPublic, string? sort, int page)
    {
        var filter = new DiaryFilter
        {
            From = from, To = to, Type = type, AuthorId = authorId, AnimalId = animalId,
            Search = search, IncludeNonPublic = includeNonPublic, Sort = sort ?? "date_desc",
            Page = page, PageSize = 10
        };

        Vm = new DiaryPageViewModel
        {
            Page = await _diary.GetPagedAsync(filter, canSeeNonPublic: true),
            Filter = filter.Normalized(),
            Authors = await _db.Users.AsNoTracking().OrderBy(u => u.DisplayName).ToListAsync(),
            Animals = await _db.Animals.AsNoTracking().OrderBy(a => a.Id).ToListAsync(),
            CanSeeNonPublic = true
        };

        ViewData["Title"] = "Дневник (служебный)";
    }
}
