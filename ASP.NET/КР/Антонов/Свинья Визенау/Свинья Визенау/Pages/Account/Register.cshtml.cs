using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Свинья_Визенау.Models;

namespace Свинья_Визенау.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public RegisterModel(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string? DisplayName { get; set; }
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                EmailConfirmed = true,
                DisplayName = Input.DisplayName
            };

            var result = await userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Visitor");
                await signInManager.SignInAsync(user, false);
                return RedirectToPage("/Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return Page();
        }
    }
}
