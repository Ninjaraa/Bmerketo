using static WebApp.ViewModels.ProductViewModel;

namespace WebApp.ViewModels
{
    public class GridCollectionViewModel
    {
        public string Title { get; set; } = null!;
        public List<string> Categories { get; set; } = null!;
        public List<ProductViewModel> ProductItems { get; set; } = null!;
        public string LoadMoreUrl { get; set; } = null!;
        public bool LoadMore { get; set; } = false;
        public bool IsLast { get; set; } = false;
        public ProductType Type { get; set; }
    }
}