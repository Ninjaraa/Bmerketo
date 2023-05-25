using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;
using static WebApp.ViewModels.ProductViewModel;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService;

        public HomeController(HomeService homeService)
        {
            _homeService = homeService;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = await _homeService.GetHomeIndexViewModelAsync();
            return View(viewModel);
        }
    }
}