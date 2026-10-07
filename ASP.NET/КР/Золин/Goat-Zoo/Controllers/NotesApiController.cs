using Goat_Zoo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Goat_Zoo.Controllers
{
    public class NotesApiController : Controller
    {
        private readonly GoatZooContext db;

        public NotesApiController(GoatZooContext db)
        {
            this.db = db;
        }

        private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = CurrentUserId;
            if (userId == null)
                return Unauthorized(new { error = "Не авторизован" });

            var notes = await db.Notes
                .Where(n => n.UserId == userId.Value)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new
                {
                    n.Id,
                    n.Type,
                    n.Title,
                    n.Content,
                    n.CreatedAt
                })
                .ToListAsync();

            return Json(notes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteInput input)
        {
            var userId = CurrentUserId;
            if (userId == null)
                return Unauthorized(new { error = "Не авторизован" });

            if (input == null || string.IsNullOrWhiteSpace(input.Type))
                return BadRequest(new { error = "Категория обязательна" });

            if (string.IsNullOrWhiteSpace(input.Title) && string.IsNullOrWhiteSpace(input.Content))
                return BadRequest(new { error = "Заполните заголовок или описание" });

            var note = new Note
            {
                UserId = userId.Value,
                Type = input.Type.Trim(),
                Title = (input.Title ?? "").Trim(),
                Content = (input.Content ?? "").Trim(),
                CreatedAt = DateTime.Now
            };

            db.Notes.Add(note);
            await db.SaveChangesAsync();

            return Json(new
            {
                note.Id,
                note.Type,
                note.Title,
                note.Content,
                note.CreatedAt
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var userId = CurrentUserId;
            if (userId == null)
                return Unauthorized(new { error = "Не авторизован" });

            var note = await db.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId.Value);

            if (note == null) return NotFound();

            db.Notes.Remove(note);
            await db.SaveChangesAsync();
            return Json(new { ok = true });
        }

        public class NoteInput
        {
            public string Type { get; set; } = "";
            public string Title { get; set; } = "";
            public string Content { get; set; } = "";
        }
    }
}
