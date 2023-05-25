using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class UserEditViewModel
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Company { get; set; }
        public string? NewPassword { get; set; }

        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } = null!;

        [Display(Name = "Role")]
        public string RoleId { get; set; }
        public List<SelectListItem> AvailableRoles { get; set; }

        [Display(Name = "Roles")]
        public List<string> Roles { get; set; }
        public string? RoleName { get; set; }
        public bool? AgreeToTerms { get; set; }

        public byte[]? ProfileImageData { get; set; }
        public string? ImageMimeType { get; set; }
        public string? ContentType { get; set; }
    }
}