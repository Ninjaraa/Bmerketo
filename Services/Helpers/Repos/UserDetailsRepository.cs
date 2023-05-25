using WebApp.Contexts;
using WebApp.Models.Entities;

namespace WebApp.Services.Helpers.Repos
{
    public class UserDetailsRepository : Repository<UserDetailsEntity>
    {
        public UserDetailsRepository(IdentityContext context) : base(context)
        {
        }
    }
}