using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Identity;

namespace WebApp.Controllers
{
    public class LogOutController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LogOutController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // Handle sign out
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            return LocalRedirect("/");
        }
    }
}