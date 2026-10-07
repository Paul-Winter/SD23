using Microsoft.AspNetCore.Identity;

namespace Свинья_Визенау.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
    }
}
