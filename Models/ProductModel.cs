using WebApp.Models.Entities;
using static WebApp.ViewModels.ProductViewModel;

namespace WebApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? SKU { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }
        public string? ImageBase64 { get; set; }
        public string? CategoryName { get; set; }
        public ProductCategoryEntity? Category { get; set; }
        public List<ProductEntity>? ProductItems { get; set; }
        public PriceEntity? Price { get; set; }
        public bool IsIcon { get; set; }
        public ProductType Type { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public List<ProductModel> RelatedProducts { get; set; } = new List<ProductModel>();
    }
}