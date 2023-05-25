using WebApp.Models.Entities;

namespace WebApp.Models.Identity
{
    public class IdentityModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        public string? Mobile { get; set; }
        public string City { get; set; }

        public byte[]? ProfileImageData { get; set; }
        public string? ImageMimeType { get; set; }
        public string? Company { get; set; }
        public bool AgreeToTerms { get; set; }

        public AddressEntity? Address { get; set; }
        public UserDetailsEntity? UserDetails { get; set; }
        public AppUser? User { get; set; } = null!;

    }
}
