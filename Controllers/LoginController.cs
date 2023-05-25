using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;
using WebApp.Services.Helpers;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly AuthenticationService _auth;

        public LoginController(AuthenticationService auth)
        {
            _auth = auth;
        }

        public IActionResult Index(string ReturnUrl = null!)
        {
            var viewModel = new UserLogInViewModel();
            if (ReturnUrl != null) 
                viewModel.ReturnUrl = ReturnUrl;

            return View(viewModel);
        }

        // HttpPost to handle log in
        [HttpPost]
        public async Task<IActionResult> Index(UserLogInViewModel viewModel) 
        {

            if (ModelState.IsValid)
            {
                if (await _auth.LoginAsync(viewModel))
                return LocalRedirect(viewModel.ReturnUrl);

               ModelState.AddModelError("", "Incorrect email or password");
            }
            
            return View(viewModel);
        }
    }
}
