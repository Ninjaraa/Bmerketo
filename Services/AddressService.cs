using WebApp.Models.Entities;
using WebApp.Models.Identity;
using WebApp.Services.Helpers.Repos;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class AddressService
    {
        private readonly AddressRepository _addressRepo;

        public AddressService(AddressRepository addressRepo)
        {
            _addressRepo = addressRepo;
        }

        // Method to get or create a new address for the user when register
        public async Task<AddressEntity> GetOrCreateAsync(UserSignUpViewModel viewModel)
        {
            var entity = await _addressRepo.GetAsync(x =>
                x.StreetName == viewModel.StreetName &&
                x.PostalCode == viewModel.PostalCode &&
                x.City == viewModel.City
            );

            if (entity == null)
            {
                entity = new AddressEntity
                {
                    StreetName = viewModel.StreetName,
                    PostalCode = viewModel.PostalCode,
                    City = viewModel.City
                };

                entity = await _addressRepo.AddAsync(entity);
            }

            return entity;
        }

        public async Task AddAddressAsync(AppUser user, AddressEntity addressEntity)
        {
            addressEntity.UserId = user.Id;
            await _addressRepo.UpdateAsync(addressEntity);
        }
    }
}