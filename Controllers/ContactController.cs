using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Entities;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {

        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // HttpPost to add a new comment on the contact view
        [HttpPost]
        public async Task<IActionResult> Index(ContactIndexViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var contact = new ContactEntity
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Mobile = viewModel.Mobile,
                Company = viewModel?.Company,
                Message = viewModel.Message,
                SaveLogIn = viewModel?.SaveLogIn
            };

            await _contactService.AddContactAsync(contact);
            return RedirectToAction("Index");
        }

    }
}
