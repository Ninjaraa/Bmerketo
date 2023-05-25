using WebApp.Models;

namespace WebApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public string Title { get; set; } = "Home";
        public ShowCaseModel ShowCase { get; set; } = null!;
        public GridCollectionViewModel BestCollection { get; set; } = null!;
        public GridCollectionViewModel SummerCollection { get; set; } = null!;
        public SalesViewModel SalesCollection { get; set; } = null!;
        public TopSellViewModel TopSellCollection { get; set; } = null!;
        public TopSellPostsViewModel TopSellPosts { get; set; } = null!;

    }
}