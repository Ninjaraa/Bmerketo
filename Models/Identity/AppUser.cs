using Microsoft.AspNetCore.Identity;

namespace WebApp.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [ProtectedPersonalData]
        public string? FirstName { get; set; }

        [ProtectedPersonalData]
        public string? LastName { get; set; }
    }
}