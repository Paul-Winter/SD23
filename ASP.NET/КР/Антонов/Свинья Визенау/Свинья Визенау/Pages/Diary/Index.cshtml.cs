using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Свинья_Визенау.Data;
using Свинья_Визенау.Models;

namespace Свинья_Визенау.Pages.Diary
{
    [Authorize(Roles = "Worker,Admin")]
    public class IndexModel : PageModel
    {
        private readonly ZooContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public IndexModel(ZooContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public List<DiaryEntry> Entries { get; set; } = new();
        public List<Animal> Animals { get; set; } = new();

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            public int AnimalId { get; set; }
            public DiaryEntryType Type { get; set; }
            public string Notes { get; set; } = string.Empty;
        }

        public async Task OnGetAsync()
        {
            Animals = await context.Animals.ToListAsync();
            Entries = await context.DiaryEntries
                .Include(e => e.Animal)
                .Include(e => e.RecordedBy)
                .OrderByDescending(e => e.RecordedAt)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            var entry = new DiaryEntry
            {
                AnimalId = Input.AnimalId,
                Type = Input.Type,
                Notes = Input.Notes,
                RecordedAt = DateTime.UtcNow,
                RecordedByUserId = userManager.GetUserId(User)
            };

            context.DiaryEntries.Add(entry);
            await context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}