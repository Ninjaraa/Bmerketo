using static WebApp.ViewModels.ProductViewModel;
using WebApp.Models;
using WebApp.Models.Entities;

namespace WebApp.ViewModels
{
    public class ProductDetailsViewModel
    {
        public int ProductId { get; set; }

        public int TagId { get; set; }
        public List<ProductModel> ProductItems { get; set; } = new List<ProductModel>();
        public string Title { get; set; } = null!;
        public ProductType Type { get; set; }
        public ProductModel Product { get; set; }
        public List<ProductModel> RelatedProducts { get; set; } = new List<ProductModel>();

        public List<string> Tags { get; set; } = new List<string>();
    }    
}
