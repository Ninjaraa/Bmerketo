namespace WebApp.ViewModels
{
    public class PriceViewModel
    {
        public decimal? OrdinaryPrice { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public bool IsOriginalPrice { get; set; }
        public bool IsDiscountPrice { get; set; }
        public bool IsOrdinaryPrice { get; set; }
        public bool IsIcon { get; set; }
        public bool IsTitleCenter { get; set; }
    }
}
