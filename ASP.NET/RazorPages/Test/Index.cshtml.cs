using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RazorPages.Pages.Test
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        public string Message { get; set; } = "";
        //public IndexModel()
        //{
        //    Message = "Hello, World!";
        //}
        public string PrintMessage()
        {
            return $"Today: {DateTime.Now.ToLongDateString()}";
        }

        public void OnGet()
        {
            Message = "Введите логин:";
        }
        public void OnPost(string login)
        {
            Message = $"Login: {login}";
        }
    }
}
