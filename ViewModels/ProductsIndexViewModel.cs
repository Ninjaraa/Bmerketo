using WebApp.Models;
using WebApp.Models.Entities;
using static WebApp.ViewModels.ProductViewModel;

namespace WebApp.ViewModels
{
    public class ProductsIndexViewModel
    {
        public string Title { get; set; } = null!;
        public ProductType Type { get; set; }
        public List<ProductModel> ProductItems { get; set; } = new List<ProductModel>();

        public List<ProductViewModel> SalesCard { get; set; } = new List<ProductViewModel>();

    }
}
