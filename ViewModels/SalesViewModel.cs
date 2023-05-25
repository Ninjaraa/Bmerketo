using static WebApp.ViewModels.ProductViewModel;

namespace WebApp.ViewModels
{
    public class SalesViewModel
    {
        public string Title { get; set; } = null!;
        public List<ProductViewModel> ProductItems { get; set; } = null!;
        public ProductType Type { get; set; }

    }
}
