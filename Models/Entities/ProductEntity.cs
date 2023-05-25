using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Entities
{

    public class ProductEntity
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? SKU { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string ProductName { get; set; } = null!;

        [StringLength(150)]
        [Column(TypeName = "nvarchar(150)")]
        public string? Description { get; set; }

        public byte[]? ImageData { get; set; }

        public string? ImageMimeType { get; set; }

        public PriceEntity? Price { get; set; }

        public ProductCategoryEntity? Category { get; set; }

        public ICollection<TagEntity> Tags { get; set; } = new HashSet<TagEntity>();

        [NotMapped]
        public string ComputedSKU => $"{Id}-{ProductName.Replace(" ", "-")}".ToLowerInvariant();

        public static implicit operator ProductModel(ProductEntity entity)
        {
            return new ProductModel
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Description = entity.Description,
                ImageData = entity.ImageData,
                ImageMimeType = entity.ImageMimeType,
                Category = entity.Category,
            };
        }
    }
}