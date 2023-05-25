using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApp.Contexts;

namespace WebApp.Models.Identity
{

    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IdentityContext _context;

        public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager, IOptions<IdentityOptions> optionsAccessor, IdentityContext context) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
            _context = context;
        }

        // Additional claims to add DisplayName and to authorize the user on role
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);

            claimsIdentity.AddClaim(new Claim("DisplayName", $"{user.FirstName} {user.LastName}"));

            // Thank you Elin for helping out with this part <3
            var roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return claimsIdentity;
        }
    }
}