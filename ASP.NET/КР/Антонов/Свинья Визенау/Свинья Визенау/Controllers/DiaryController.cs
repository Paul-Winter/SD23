using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Свинья_Визенау.Data;
using Свинья_Визенау.Models;

namespace Свинья_Визенау.Controllers
{
    [ApiController]
    [Route("api/animals/{animalId:int}/diary")]
    [Authorize(Roles = "Worker,Admin")]
    public class DiaryController : ControllerBase
    {
        private readonly ZooContext context;

        public DiaryController(ZooContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int animalId)
        {
            var animalExists = await context.Animals.AnyAsync(a => a.Id == animalId);
            if (!animalExists) return NotFound();

            var entries = await context.DiaryEntries
                .Where(d => d.AnimalId == animalId)
                .OrderByDescending(d => d.RecordedAt)
                .Select(d => new
                {
                    d.Id,
                    d.Type,
                    d.Notes,
                    d.RecordedAt,
                    RecordedBy = d.RecordedBy != null ? d.RecordedBy.DisplayName : null
                })
                .ToListAsync();

            return Ok(entries);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int animalId, [FromBody] DiaryCreateDto dto)
        {
            var animalExists = await context.Animals.AnyAsync(a => a.Id == animalId);
            if (!animalExists) return NotFound();

            var entry = new DiaryEntry
            {
                AnimalId = animalId,
                Type = dto.Type,
                Notes = dto.Notes,
                RecordedAt = DateTime.UtcNow,
                RecordedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            context.DiaryEntries.Add(entry);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { animalId }, new { entry.Id });
        }

        public class DiaryCreateDto
        {
            public DiaryEntryType Type { get; set; }
            public string Notes { get; set; } = string.Empty;
        }
    }
}