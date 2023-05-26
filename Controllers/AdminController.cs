using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels;
using WebApp.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Sysadmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityContext _identityContext;

        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IdentityContext identityContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityContext = identityContext;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel
                {
                    User = user,
                    Roles = roles
                };
                userViewModels.Add(userViewModel);
            }
            return View(userViewModels);
        }

        // HttpGet to display user information in the admin view
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var address = await _identityContext.AspNetAddresses.FirstOrDefaultAsync(a => a.UserId == user.Id);
            var userDetails = await _identityContext.AspNetUserDetails.FirstOrDefaultAsync(a => a.UserId == user.Id);

            var role = await _userManager.GetRolesAsync(user);
            var roleName = role.FirstOrDefault();

            var userRoles = await _userManager.GetRolesAsync(user);
            var availableRoles = _roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userRoles.Contains(r.Name)
                }).ToList();

            var model = new UserEditViewModel
            {
                Id = user.Id,
                AvailableRoles = availableRoles,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                StreetName = address?.StreetName,
                PostalCode = address.PostalCode,
                PhoneNumber = user?.PhoneNumber,
                City = address.City,
                RoleName = roleName,
                Company = userDetails?.Company,
                ProfileImageData = userDetails?.ProfileImageData ?? Array.Empty<byte>(),
                ImageMimeType = userDetails?.ImageMimeType,
                ContentType = userDetails?.ProfileImageData != null ? Convert.ToBase64String(userDetails.ProfileImageData) : null
            };

            return View(model);
        }

        // HttpPost for editing a users role, and/or password
        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditViewModel model)
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

            var currentRoles = (await _userManager.GetRolesAsync(user)) ?? new List<string>();
            if (currentRoles == null)
            {
                currentRoles = new List<string>();
            }

            var rolesToAdd = model.Roles.Where(r => !currentRoles.Contains(r)).ToList();
            var rolesToRemove = currentRoles.Where(r => !model.Roles.Contains(r)).ToList();

            if (rolesToAdd.Any() || rolesToRemove.Any())
            {
                await _userManager.AddToRolesAsync(user, rolesToAdd);
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to update user's password");
                    return View(model);
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update user");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        //HttpGet to display user information in the admin view
       [HttpGet]
        public IActionResult CreateUser()
        {
            var roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToList();

            var model = new CreateUserViewModel
            {
                AvailableRoles = roles
            };

            return View(model);
        }

        //HttpPost to create a new user from the admin view
       [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, model.Roles);

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            var roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name,
                Selected = model.Roles.Contains(r.Name)
            }).ToList();
            model.AvailableRoles = roles;

            return View(model);
        }
    }
}