using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Identity;
using WebApp.ViewModels;

namespace WebApp.Services.Helpers
{
    public class AuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AddressService _addressService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserDetailsService _userDetailsService;

        public AuthenticationService(UserManager<AppUser> userManager, AddressService addressService, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, UserDetailsService userDetailsService)
        {
            _userManager = userManager;
            _addressService = addressService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userDetailsService = userDetailsService;
        }

        // When creating a new user the first user will be assigned to the role "sysadmin"
        // All others will be assigned the role "user"
        public async Task<bool> RegisterUserAsync(UserSignUpViewModel viewModel)
        {
            AppUser appUser = viewModel;
            var roleName = "User";

            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Sysadmin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
                roleName = "Sysadmin";
            }
            else if (!await _userManager.Users.AnyAsync())
            {
                roleName = "Sysadmin";
            }

            var result = await _userManager.CreateAsync(appUser, viewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, roleName);

                var addressEntity = await _addressService.GetOrCreateAsync(viewModel);
                if (addressEntity != null)
                {
                    await _addressService.AddAddressAsync(appUser, addressEntity);

                    var userDetails = viewModel;
                    userDetails.Company = viewModel.Company;
                    userDetails.AgreeToTerms = viewModel.AgreeToTerms;

                    if (viewModel.ProfileImage != null && viewModel.ProfileImage.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        viewModel.ProfileImage.CopyTo(ms);
                        userDetails.ProfileImageData = ms.ToArray();
                        userDetails.ImageMimeType = viewModel.ProfileImage.ContentType;
                        userDetails.ContentType = viewModel.ProfileImage.ContentType;
                    }

                    await _userDetailsService.AddUserDetailsAsync(appUser, userDetails);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        // Check email and password to sign in the right user
        public async Task<bool> LoginAsync(UserLogInViewModel viewModel)
        {
            var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == viewModel.Email);
            if (appUser != null)
            {
                var result = await _signInManager.PasswordSignInAsync(appUser, viewModel.Password, viewModel.RememberMe, false);
                return result.Succeeded;
            }

            return false;
        }
    }
}