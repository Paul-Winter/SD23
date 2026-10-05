using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StavZoo.Data;
using StavZoo.Models;

namespace StavZoo.Controllers;

[ApiController]
[Route("api/lion")]
public class LionApiController : ControllerBase
{
    private readonly ZooDbContext db;

    public LionApiController(ZooDbContext db)
    {
        this.db = db;
    }

    [HttpGet("{name}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLion(string name)
    {
        var lion = await db.Lions.FirstOrDefaultAsync(l => l.Name.ToLower() == name.ToLower());
        if (lion == null) return NotFound();
        return Ok(lion);
    }

    [HttpGet("{name}/diary")]
    [Authorize(Roles = "Zookeeper")]
    public async Task<IActionResult> GetDiary(string name)
    {
        var entries = await db.LionDiaryEntries
            .Where(e => e.Lion.Name == name)
            .OrderByDescending(e => e.RecordedAt)
            .ToListAsync();
        return Ok(entries);
    }

    [HttpPost("{name}/diary")]
    [Authorize(Roles = "Zookeeper")]
    public async Task<IActionResult> AddDiaryEntry(string name, string entryType, string notes)
    {
        var lion = await db.Lions.FirstOrDefaultAsync(l => l.Name == name);
        if (lion == null) return NotFound();

        var entry = new LionDiaryEntry
        {
            LionId = lion.Id,
            EntryType = entryType,
            Notes = notes,
            RecordedAt = DateTime.UtcNow,
            RecordedBy = User.Identity?.Name ?? "unknown"
        };

        db.LionDiaryEntries.Add(entry);
        await db.SaveChangesAsync();
        return Ok(entry);
    }
}