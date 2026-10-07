using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Свинья_Визенау.Models;

namespace Свинья_Визенау.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid) return Page();

            var result = await signInManager.PasswordSignInAsync(
                Input.Email, Input.Password, false, false);

            if (result.Succeeded)
                return LocalRedirect(returnUrl ?? "/");

            ModelState.AddModelError("", "Неверный email или пароль");
            return Page();
        }
    }
}