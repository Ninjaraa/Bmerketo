using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Entities
{
    public class PriceEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? OrdinaryPrice { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? OriginalPrice { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? DiscountPrice { get; set; }

        public ProductEntity? Product { get; set; }
        public bool IsOriginalPrice { get; set; }
        public bool IsDiscountPrice { get; set; }
        public bool IsOrdinaryPrice { get; set; }

        public PriceEntity(decimal? ordinaryPrice, decimal? originalPrice, decimal? discountPrice)
        {
            OrdinaryPrice = ordinaryPrice;
            OriginalPrice = originalPrice;
            DiscountPrice = discountPrice;
            IsOriginalPrice = originalPrice.HasValue;
            IsDiscountPrice = discountPrice.HasValue;
            IsOrdinaryPrice = !originalPrice.HasValue && !discountPrice.HasValue;
        }
    }
}