using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Please enter the first name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the last name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter a email address")]
        public string? Email { get; set; }

        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please confirm the password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string? ConfirmPassword { get; set; }

        public List<SelectListItem>? AvailableRoles { get; set; }

        [Display(Name = "Roles")]
        [Required(ErrorMessage = "Please select at least one role.")]
        public List<string>? Roles { get; set; }
    }
}