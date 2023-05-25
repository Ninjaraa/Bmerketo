using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ContactIndexViewModel
    {

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } 

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } 

        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        public string? Mobile { get; set; }

        [Display(Name = "Company")]
        public string? Company { get; set; }

        public bool SaveLogIn { get; set; }

        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Nothing to say? A message is required")]
        public string Message { get; set; }

    }
}
