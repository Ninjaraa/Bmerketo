using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels

{
    public class ProductRegistrationViewModel
    {
        [Display(Name = "Product Name *")]
        [Required(ErrorMessage = "Product name is required")]
        [MinLength(3, ErrorMessage = "Product Name must be longer than two characters")]
        public string ProductName { get; set; }
        public string? SKU { get; set; }

        [Display(Name = "Product Description")]
        public string? Description { get; set; }

        [Display(Name = "Price")]
        public string? Price { get; set; } = string.Empty;

        [Display(Name = "Discount Price")]
        public decimal? DiscountPrice { get; set; }

        [Display(Name = "Ordinary Price")]
        public decimal? OrdinaryPrice { get; set; }

        [Display(Name = "Original Price")]
        public decimal? OriginalPrice { get; set; }

        [Display(Name = "Category*")]
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        [Display(Name = "Image")]
        public byte[]? ProductImage { get; set; }
        public string? ImageMimeType { get; set; }
        public IFormFile? Image { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}