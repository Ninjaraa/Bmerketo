using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.Entities;

namespace WebApp.Models.Entities
{
    public class ProductTagEntity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;

        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }
        public TagEntity Tag { get; set; } = null!;
    }
}
