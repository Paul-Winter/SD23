using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Pages.Wolf;

/// <summary>Наблюдение за вольером через веб-камеру (HLS / MJPEG / кадры JPEG / демо-режим).</summary>
public class WebcamModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly WebcamService _camera;

    public WebcamModel(AppDbContext db, WebcamService camera)
    {
        _db = db;
        _camera = camera;
    }

    public Animal Animal { get; private set; } = new();
    public MediaItem? Webcam { get; private set; }
    public WebcamStatus Camera { get; private set; } = new();
    public List<MediaItem> Videos { get; private set; } = new();
    public List<MediaItem> Photos { get; private set; } = new();

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        var animal = await _db.Animals.Include(a => a.Media).AsNoTracking()
            .FirstOrDefaultAsync(a => a.SubdomainSlug == slug);
        if (animal == null) return NotFound();

        Animal = animal;
        Webcam = animal.Media.FirstOrDefault(m => m.Kind == MediaKinds.Webcam);
        Videos = animal.Media.Where(m => m.Kind == MediaKinds.Video).ToList();
        Photos = animal.Media.Where(m => m.Kind == MediaKinds.Photo).ToList();

        var status = _camera.GetStatus();
        // Подпись к камере (название/описание из БД) + текущий режим из конфигурации.
        if (Webcam != null && !string.IsNullOrWhiteSpace(Webcam.Description))
            status.SourceDescription = $"{status.SourceDescription}";
        Camera = status;

        ViewData["Title"] = $"Веб-камера — {animal.Name}";
        ViewData["AnimalSlug"] = animal.SubdomainSlug;
        ViewData["AnimalName"] = animal.Name;
        return Page();
    }
}
