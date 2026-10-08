using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Coati_Space_Project.Models;

namespace Coati_Space_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DiaryController : Controller
    {
        ApplicationContext db;

        public DiaryController(ApplicationContext context)
        {
            db = context;
        }

        // Список событий с возможностью фильтрации по типу события
        public async Task<IActionResult> Index(string? filterType)
        {
            var query = db.DiaryEntries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterType) && filterType != "Все")
            {
                query = query.Where(e => e.EventType == filterType);
            }

            var entries = await query.OrderByDescending(e => e.EventDate).ToListAsync();
            ViewBag.CurrentFilter = filterType ?? "Все";
            return View(entries);
        }

        // GET: Создание записи
        [HttpGet]
        public IActionResult Create()
        {
            var entry = new DiaryEntry
            {
                CoatiId = 1,
                EventDate = DateTime.Now,
                EventType = "Кормление",
                Author = User.Identity?.Name ?? "Кипер зоопарка"
            };
            return View(entry);
        }

        // POST: Создание записи
        [HttpPost]
        public async Task<IActionResult> Create(DiaryEntry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Title))
            {
                ModelState.AddModelError("Title", "Укажите заголовок события.");
            }

            if (string.IsNullOrWhiteSpace(entry.Description))
            {
                ModelState.AddModelError("Description", "Укажите описание события.");
            }

            if (!ModelState.IsValid)
            {
                return View(entry);
            }

            if (entry.CoatiId == 0)
            {
                entry.CoatiId = 1;
            }

            if (string.IsNullOrWhiteSpace(entry.Author))
            {
                entry.Author = User.Identity?.Name ?? "Сотрудник";
            }

            db.DiaryEntries.Add(entry);
            await db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Событие «{entry.Title}» успешно добавлено в журнал!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Редактирование записи
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entry = await db.DiaryEntries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return View(entry);
        }

        // POST: Редактирование записи
        [HttpPost]
        public async Task<IActionResult> Edit(DiaryEntry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Title))
            {
                ModelState.AddModelError("Title", "Укажите заголовок события.");
            }

            if (string.IsNullOrWhiteSpace(entry.Description))
            {
                ModelState.AddModelError("Description", "Укажите описание события.");
            }

            if (!ModelState.IsValid)
            {
                return View(entry);
            }

            db.DiaryEntries.Update(entry);
            await db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Событие #{entry.Id} успешно обновлено!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Подтверждение удаления
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var entry = await db.DiaryEntries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return View(entry);
        }

        // POST: Удаление
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entry = await db.DiaryEntries.FindAsync(id);
            if (entry != null)
            {
                db.DiaryEntries.Remove(entry);
                await db.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Событие #{id} удалено из журнала наблюдений.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
