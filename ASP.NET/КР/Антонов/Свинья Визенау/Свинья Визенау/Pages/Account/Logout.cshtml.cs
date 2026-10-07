using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Свинья_Визенау.Models;


namespace Свинья_Визенау.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}