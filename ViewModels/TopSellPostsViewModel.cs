using static WebApp.ViewModels.ProductViewModel;

namespace WebApp.ViewModels
{
    public class TopSellPostsViewModel
    {
        public string? Title { get; set; }
        public List<ProductViewModel> ProductItems { get; set; } = null!;
        public ProductType Type { get; set; }
    }
}
