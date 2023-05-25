using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Helpers;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AuthenticationService _auth;

        public RegisterController(AuthenticationService auth)
        {
            _auth = auth;
        }
        public IActionResult Index()
        {
            return View();
        }

        // HttpPost to handle register a new user
        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _auth.RegisterUserAsync(viewModel);
            return RedirectToAction("Index", "Login");
        }
    }
}