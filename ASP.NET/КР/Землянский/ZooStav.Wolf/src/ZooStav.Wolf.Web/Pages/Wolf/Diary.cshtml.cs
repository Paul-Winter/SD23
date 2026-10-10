using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Pages.Wolf;

/// <summary>Публичный дневник стаи с фильтрацией по дате, типу записи, особи и тексту.</summary>
public class DiaryModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly DiaryService _diary;

    public DiaryModel(AppDbContext db, DiaryService diary)
    {
        _db = db;
        _diary = diary;
    }

    public DiaryPageViewModel Vm { get; private set; } = new();

    public async Task<IActionResult> OnGetAsync(string slug,
        DateTime? from, DateTime? to, DiaryEntryType? type, int? animalId, string? search,
        string? sort, int page = 1)
    {
        var animal = await _db.Animals.AsNoTracking().FirstOrDefaultAsync(a => a.SubdomainSlug == slug);
        if (animal == null) return NotFound();

        var filter = new DiaryFilter
        {
            From = from, To = to, Type = type, AnimalId = animalId, Search = search,
            Sort = sort ?? "date_desc", Page = page, PageSize = 8
        };

        // Посетителям показываются только публичные записи (IsPublic = true).
        var pageResult = await _diary.GetPagedAsync(filter, canSeeNonPublic: false);

        Vm = new DiaryPageViewModel
        {
            Animal = animal,
            Page = pageResult,
            Filter = filter.Normalized(),
            Animals = await _db.Animals.AsNoTracking().OrderBy(a => a.Id).ToListAsync(),
            CanSeeNonPublic = false
        };

        ViewData["Title"] = $"Дневник стаи — {animal.Name}";
        ViewData["AnimalSlug"] = animal.SubdomainSlug;
        ViewData["AnimalName"] = animal.Name;
        return Page();
    }
}
