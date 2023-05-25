using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Entities
{

    public class ProductCategoryEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Category { get; set; } = null!;

        public int ProductId { get; set; }
        public ProductEntity? Product { get; set; }

        public List<ProductEntity> RelatedProducts { get; set; } = new List<ProductEntity>();
    }
}