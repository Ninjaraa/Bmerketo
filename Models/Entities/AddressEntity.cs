using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models.Identity;

namespace WebApp.Models.Entities
{
    public class AddressEntity
    {
        public int Id { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public AppUser? User { get; set; }

    }
}
