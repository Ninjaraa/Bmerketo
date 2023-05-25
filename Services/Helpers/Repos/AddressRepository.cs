using WebApp.Contexts;
using WebApp.Models.Entities;

namespace WebApp.Services.Helpers.Repos
{
    public class AddressRepository : Repository<AddressEntity>
    {
        public AddressRepository(IdentityContext context) : base(context)
        {
        }
    }
}
