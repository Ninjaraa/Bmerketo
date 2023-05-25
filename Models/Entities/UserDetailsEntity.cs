using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models.Identity;

namespace WebApp.Models.Entities
{
    public class UserDetailsEntity
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Company { get; set; }
        public byte[]? ProfileImageData { get; set; }
        public string? ImageMimeType { get; set; } = string.Empty;
        public string? ContentType { get; set; } = string.Empty;
        public bool? AgreeToTerms { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
