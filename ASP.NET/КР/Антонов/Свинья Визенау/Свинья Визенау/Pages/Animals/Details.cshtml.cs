using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Свинья_Визенау.Data;
using Свинья_Визенау.Models;

namespace Свинья_Визенау.Pages.Animals
{
    public class DetailsModel : PageModel
    {
        private readonly ZooContext context;

        public DetailsModel(ZooContext context)
        {
            this.context = context;
        }

        public Animal Animal { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return NotFound();

            var animal = await context.Animals
                .Include(a => a.DiaryEntries)
                    .ThenInclude(d => d.RecordedBy)
                .FirstOrDefaultAsync(a => a.Slug == slug);

            if (animal == null) return NotFound();

            Animal = animal;
            return Page();
        }
    }
}