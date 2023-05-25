using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Entities
{
    public class ContactEntity
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Mobile { get; set; }

        public string? Company { get; set; }

        public bool? SaveLogIn { get; set; }

        [Required]
        public string Message { get; set; }


    }
}
