using System.ComponentModel.DataAnnotations;
using WebApp.Models.Entities;
using WebApp.Models.Identity;

namespace WebApp.ViewModels;

public class UserSignUpViewModel
{

    [Display(Name = "FirstName")]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Display(Name = "LastName")]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; } = null!;

    [Display(Name = "PhoneNumber")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^[\d\s-]+$", ErrorMessage = "Phone number can only contain numbers")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "ConfirmPassword")]
    [Required(ErrorMessage = "Confirmation of the password is required")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "StreetName")]
    [Required(ErrorMessage = "Street name is required")]
    public string StreetName { get; set; }

    [Display(Name = "PostalCode")]
    [Required(ErrorMessage = "Postal code is required")]
    [RegularExpression(@"^[\d\s-]+$", ErrorMessage = "Postal code can only contain numbers")]
    public string PostalCode { get; set; }

    [Display(Name = "City")]
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; }

    public IFormFile? ProfileImage { get; set; }
    public byte[]? ProfileImageData { get; set; }
    public string? ImageMimeType { get; set; }
    public string? ContentType { get; set; }
    public string? Company { get; set; }
    public bool AgreeToTerms { get; set; }

    public static implicit operator AppUser(UserSignUpViewModel model)
    {
        return new AppUser
        {
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };
    }
    public void UpdateUserDetails(UserDetailsEntity userDetails)
    {
        if (ProfileImage != null && ProfileImage.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                ProfileImage.CopyTo(ms);
                userDetails.ProfileImageData = ms.ToArray();
                userDetails.ImageMimeType = ProfileImage.ContentType;
                userDetails.ContentType = ProfileImage.ContentType;
            }
        }
        userDetails.Company = Company;
    }

    public static implicit operator UserDetailsEntity(UserSignUpViewModel model)
    {
        var userDetails = new UserDetailsEntity();
        model.UpdateUserDetails(userDetails);
        return userDetails;
    }
}