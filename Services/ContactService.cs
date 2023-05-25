using WebApp.Contexts;
using WebApp.Models.Entities;

namespace WebApp.Services
{
    public class ContactService
    {
        private readonly ContactContext _contactContext;

        public ContactService(ContactContext contactContext)
        {
            _contactContext = contactContext;
        }

        // Method to add a new comment when filling out the contact form
        public async Task<int> AddContactAsync(ContactEntity contact)
        {
            _contactContext.Contacts.Add(contact);
            await _contactContext.SaveChangesAsync();
            return contact.Id;
        }
    }
}