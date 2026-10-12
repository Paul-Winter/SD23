using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StavZoo.Data;

namespace StavZoo.Pages.Lion;

[Authorize(Roles = "Zookeeper")]
public class DiaryModel : PageModel
{
    private readonly ZooDbContext db;

    public DiaryModel(ZooDbContext db)
    {
        this.db = db;
    }

    public string AnimalName { get; set; } = "";
    public string Filter { get; set; } = "";
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public List<Models.LionDiaryEntry> Entries { get; set; } = new();

    public int EditId { get; set; }
    public string EditType { get; set; } = "";
    public string EditNotes { get; set; } = "";
    public string EditRecordedBy { get; set; } = "";

    public void OnGet(string animalName, int? editId, string? filter, DateTime? from, DateTime? to)
    {
        AnimalName = animalName;
        Filter = filter ?? "";
        From = from;
        To = to;

        LoadEntries(animalName, Filter, From, To);

        if (editId != null)
        {
            var entry = db.LionDiaryEntries.FirstOrDefault(e => e.Id == editId);
            if (entry != null)
            {
                EditId = entry.Id;
                EditType = entry.EntryType;
                EditNotes = entry.Notes;
                EditRecordedBy = entry.RecordedBy;
            }
        }
    }

    public IActionResult OnPost(string animalName, string entryType, string notes, string recordedBy)
    {
        var lion = db.Lions.FirstOrDefault(l => l.Slug == animalName);
        if (lion == null) return NotFound();

        var entry = new Models.LionDiaryEntry
        {
            LionId = lion.Id,
            EntryType = entryType,
            Notes = notes,
            RecordedAt = DateTime.UtcNow,
            RecordedBy = string.IsNullOrWhiteSpace(recordedBy)
                ? (User.Identity?.Name ?? "unknown")
                : recordedBy
        };

        db.LionDiaryEntries.Add(entry);
        db.SaveChanges();

        return Redirect($"/stav-zoo/{animalName}/diary");
    }

    public IActionResult OnPostEdit(int id, string animalName, string entryType, string notes, string recordedBy)
    {
        var entry = db.LionDiaryEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return NotFound();

        entry.EntryType = entryType;
        entry.Notes = notes;
        entry.RecordedBy = string.IsNullOrWhiteSpace(recordedBy) ? entry.RecordedBy : recordedBy;
        db.SaveChanges();

        return Redirect($"/stav-zoo/{animalName}/diary");
    }

    public IActionResult OnPostDelete(int id, string animalName)
    {
        var entry = db.LionDiaryEntries.FirstOrDefault(e => e.Id == id);
        if (entry != null)
        {
            db.LionDiaryEntries.Remove(entry);
            db.SaveChanges();
        }

        return Redirect($"/stav-zoo/{animalName}/diary");
    }

    private void LoadEntries(string animalName, string filter, DateTime? from, DateTime? to)
    {
        var query = db.LionDiaryEntries
            .Where(e => e.Lion.Slug == animalName);

        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(e => e.EntryType == filter);
        }

        if (from.HasValue)
        {
            query = query.Where(e => e.RecordedAt >= from.Value.Date);
        }

        if (to.HasValue)
        {
            query = query.Where(e => e.RecordedAt < to.Value.Date.AddDays(1));
        }

        Entries = query
            .OrderByDescending(e => e.RecordedAt)
            .ToList();
    }
}