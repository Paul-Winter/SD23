using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Свинья_Визенау.Data;
using Свинья_Визенау.Models;

namespace Свинья_Визенау.Pages.Donate
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ZooContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public IndexModel(ZooContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public List<Donation> RecentDonations { get; set; } = new();

        public class InputModel
        {
            public decimal Amount { get; set; } = 100;
            public string Purpose { get; set; } = "Корм";
            public string? DonorName { get; set; }
            public string? Message { get; set; }
        }

        public async Task OnGetAsync()
        {
            RecentDonations = await context.Donations
                .OrderByDescending(d => d.CreatedAt)
                .Take(20)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var donation = new Donation
            {
                Amount = Input.Amount,
                Purpose = Input.Purpose,
                DonorName = Input.DonorName ?? User.Identity?.Name,
                Message = Input.Message,
                CreatedAt = DateTime.UtcNow,
                UserId = userManager.GetUserId(User)
            };

            context.Donations.Add(donation);
            await context.SaveChangesAsync();

            TempData["Success"] = $"Спасибо! Ваш донат {donation.Amount}₽ принят.";
            return RedirectToPage();
        }
    }
}
