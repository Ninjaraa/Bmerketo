using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }
        public string? SKU { get; set; }

        [Display(Name = "Product Description")]
        public string? Description { get; set; }

        [Display(Name = "Price")]
        public string? Price { get; set; }

        [Display(Name = "Discount Price")]
        public decimal? DiscountPrice { get; set; }

        [Display(Name = "Ordinary Price")]
        public decimal? OrdinaryPrice { get; set; }

        [Display(Name = "Original Price")]
        public decimal? OriginalPrice { get; set; }

        [Display(Name = "Category")]
        public string? Category { get; set; }

        [Display(Name = "Upload image")]
        public byte[]? ProductImage { get; set; }
        public string? ImageMimeType { get; set; }
        public IFormFile? Image { get; set; }
        public List<string> Tags { get; set; } = new List<string>();

        public int TagId { get; set; }

        public List<ProductModel> ProductItems { get; set; } = new List<ProductModel>();
    }
}
