using WebApp.Models.Identity;

namespace WebApp.ViewModels
{

    // Used by Admin / Index to display the user info
    public class UserViewModel
    {
        public AppUser User { get; set; }
        public IList<string> Roles { get; set; }
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RoleName { get; set; }

        public string? Company { get; set; }
        public bool? AgreeToTerms { get; set; }

        public byte[]? ProfileImageData { get; set; }
        public string? ImageMimeType { get; set; }
        public string? ContentType { get; set; }

    }
}