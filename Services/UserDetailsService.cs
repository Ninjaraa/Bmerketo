using WebApp.Contexts;
using WebApp.Models.Entities;
using WebApp.Models.Identity;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class UserDetailsService
    {
        private readonly IdentityContext _identityContext;

        public UserDetailsService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        // Method to handle additional data for the user
        public async Task AddUserDetailsAsync(AppUser user, UserSignUpViewModel viewModel)
        {
            if (viewModel.ProfileImage == null || viewModel.ProfileImage.Length == 0)
            {
                return;
            }

            using var stream = new MemoryStream();
            await viewModel.ProfileImage.CopyToAsync(stream);

            var userDetailsEntity = new UserDetailsEntity
            {
                Company = viewModel.Company,
                ProfileImageData = stream.ToArray(),
                ImageMimeType = viewModel.ImageMimeType,
                ContentType = viewModel.ContentType,
                AgreeToTerms = viewModel.AgreeToTerms,
                UserId = user.Id
            };

            _identityContext.Add(userDetailsEntity);
            await _identityContext.SaveChangesAsync();
        }
    }
}