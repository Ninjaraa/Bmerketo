using WebApp.Models;
using WebApp.Models.Entities;

namespace WebApp.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; } = null!;
        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }
        public string? ImageBase64 { get; set; }
        public IFormFile? Image { get; set; }

        public PriceViewModel Price { get; set; } = new PriceViewModel();
        public ProductCategoryModel Category { get; set; } = new ProductCategoryModel();
        public List<string> Tags { get; set; } = new List<string>();

        public ProductType Type { get; set; }
        public enum ProductType
        {
            Type1,
            Type2,
            Type3
        }

        // Sales card
        public string Headline { get; set; } = null!;
        public string SalesTitle { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Ingress { get; set; } = null!;

        public bool IsTitleCenter { get; set; }
        public bool IsIcon { get; set; }

        public string CardTextClass { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Comments { get; set; } = null!;

    }

}