using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Models.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IdentityContext _identityContext;

        public AccountController(UserManager<AppUser> userManager, IdentityContext identityContext)
        {
            _userManager = userManager;
            _identityContext = identityContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var address = await _identityContext.AspNetAddresses.FirstOrDefaultAsync(a => a.UserId == user.Id);
            var userDetails = await _identityContext.AspNetUserDetails.FirstOrDefaultAsync(a => a.UserId == user.Id);

            var role = await _userManager.GetRolesAsync(user);
            var roleName = role.FirstOrDefault();

            var viewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                StreetName = address?.StreetName,
                PostalCode = address?.PostalCode,
                PhoneNumber = user?.PhoneNumber,
                City = address?.City,
                RoleName = roleName,
                Company = userDetails?.Company,

                ProfileImageData = userDetails?.ProfileImageData ?? Array.Empty<byte>(),
                ImageMimeType = userDetails?.ImageMimeType,
                ContentType = userDetails?.ProfileImageData != null ? Convert.ToBase64String(userDetails.ProfileImageData) : null

            };

            return View(viewModel);
        }

        // HttpGet to display detailed information in the account view

        [HttpGet]
        public async Task<IActionResult> EditProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var address = await _identityContext.AspNetAddresses.FirstOrDefaultAsync(a => a.UserId == user.Id);
            var userDetails = await _identityContext.AspNetUserDetails.FirstOrDefaultAsync(a => a.UserId == user.Id);

            var model = new ProfileEditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                StreetName = address?.StreetName,
                PostalCode = address?.PostalCode,
                PhoneNumber = user?.PhoneNumber,
                City = address?.City,
                Company = userDetails?.Company,

                ProfileImageData = userDetails?.ProfileImageData ?? Array.Empty<byte>(),
                ImageMimeType = userDetails?.ImageMimeType,
                ContentType = userDetails?.ProfileImageData != null ? Convert.ToBase64String(userDetails.ProfileImageData) : null

            };

            return View(model);
        }

        // HttpPost for a user to edit their own profile. Separate method since the user should not be able to change their role.

        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileEditViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(model.FirstName))
            {
                user.FirstName = model.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(model.UserName) && user.UserName != model.UserName)
            {
                user.UserName = model.UserName;
            }

            if (!string.IsNullOrWhiteSpace(model.Email) && user.Email != model.Email)
            {
                user.Email = model.Email;
            }

            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to update password");
                    return View(model);
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update");
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}