using WebApp.Contexts;
using static WebApp.ViewModels.ProductViewModel;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class HomeService
    {
        private readonly ProductService _productService;

        public HomeService(ProductService productService)
        {
            _productService = productService;
        }

        // Might be a weird way of doing this
        // I wanted to try something different to render out specific parts when using the same partial for the product cards.
        public async Task<HomeIndexViewModel> GetHomeIndexViewModelAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            var bestCollectionProducts = products.Where(p => p.CategoryName == "BestCollection").ToList();
            var summerCollectionProducts = products.Where(p => p.CategoryName == "SummerCollection").ToList();
            var salesProducts = products.Where(p => p.CategoryName == "SalesCollection").ToList();
            var topSellProducts = products.Where(p => p.CategoryName == "TopSellCollection").ToList();
            var topSellPostProducts = products.Where(p => p.CategoryName == "TopSellPostCollection").ToList();

            var viewModel = new HomeIndexViewModel
            {
                BestCollection = new GridCollectionViewModel
                {
                    Title = "Best Collection",
                    Categories = new List<string> { "All", "Bag", "Dress", "Decoration", "Essentials", "Interior", "Laptop", "Mobile", "Beauty" },
                    ProductItems = bestCollectionProducts.Select((product, index) => new ProductViewModel
                    {
                        SKU = product.SKU,
                        Id = product.Id,
                        ProductName = product.ProductName,
                        Price = new PriceViewModel
                        {
                            OriginalPrice = product.Price?.OriginalPrice,
                            DiscountPrice = product.Price?.DiscountPrice,
                            OrdinaryPrice = product.Price?.OrdinaryPrice,
                            IsOrdinaryPrice = product.Price?.OrdinaryPrice != null || (product.Price?.OriginalPrice == null && product.Price?.DiscountPrice == null),
                            IsOriginalPrice = product.Price?.OriginalPrice != null,
                            IsDiscountPrice = product.Price?.DiscountPrice != null,
                        },
                        IsIcon = index == 1,
                        IsTitleCenter = false,
                        ImageData = product.ImageData,
                        ImageMimeType = product.ImageMimeType,
                        ImageBase64 = Convert.ToBase64String(product.ImageData),
                        Type = ProductType.Type1,

                    }).ToList()
                },

                SummerCollection = new GridCollectionViewModel
                {
                    LoadMoreUrl = "/",
                    LoadMore = true,
                    ProductItems = summerCollectionProducts.Select(product => new ProductViewModel
                    {
                        SKU = product.SKU,
                        Id = product.Id,
                        ProductName = product.ProductName,
                        Price = new PriceViewModel
                        {
                            OriginalPrice = product.Price?.OriginalPrice,
                            DiscountPrice = product.Price?.DiscountPrice,
                            OrdinaryPrice = product.Price?.OrdinaryPrice,
                            IsOrdinaryPrice = product.Price?.OrdinaryPrice != null || (product.Price?.OriginalPrice == null && product.Price?.DiscountPrice == null),
                            IsOriginalPrice = product.Price?.OriginalPrice != null,
                            IsDiscountPrice = product.Price?.DiscountPrice != null,
                        },
                        IsIcon = false,
                        IsTitleCenter = false,
                        ImageData = product.ImageData,
                        ImageMimeType = product.ImageMimeType,
                        ImageBase64 = Convert.ToBase64String(product.ImageData),
                        Type = ProductType.Type1,

                    }).ToList()
                },

                SalesCollection = new SalesViewModel
                {
                    Title = "",
                    ProductItems = salesProducts.Select(product => new ProductViewModel
                    {
                        SKU = product.SKU,
                        Id = product.Id,
                        ProductName = product.ProductName,
                        Price = new PriceViewModel
                        {
                            OriginalPrice = product.Price?.OriginalPrice,
                            DiscountPrice = product.Price?.DiscountPrice,
                            OrdinaryPrice = product.Price?.OrdinaryPrice,
                            IsOrdinaryPrice = product.Price?.OrdinaryPrice != null || (product.Price?.OriginalPrice == null && product.Price?.DiscountPrice == null),
                            IsOriginalPrice = product.Price?.OriginalPrice != null,
                            IsDiscountPrice = product.Price?.DiscountPrice != null,
                        },
                        IsIcon = true,
                        IsTitleCenter = false,
                        CardTextClass = "card-text with-icon",
                        ImageData = product.ImageData,
                        ImageMimeType = product.ImageMimeType,
                        ImageBase64 = Convert.ToBase64String(product.ImageData),
                        Type = ProductType.Type1,

                    }).ToList()
                },

                TopSellCollection = new TopSellViewModel
                {
                    Title = "Top selling products in this week",
                    ProductItems = topSellProducts.Select(product => new ProductViewModel
                    {
                        SKU = product.SKU,
                        Id = product.Id,
                        ProductName = product.ProductName,
                        Price = new PriceViewModel
                        {
                            OriginalPrice = product.Price?.OriginalPrice,
                            DiscountPrice = product.Price?.DiscountPrice,
                            OrdinaryPrice = product.Price?.OrdinaryPrice,
                            IsOrdinaryPrice = product.Price?.OrdinaryPrice != null || (product.Price?.OriginalPrice == null && product.Price?.DiscountPrice == null),
                            IsOriginalPrice = product.Price?.OriginalPrice != null,
                            IsDiscountPrice = product.Price?.DiscountPrice != null,
                        },
                        IsIcon = false,
                        IsTitleCenter = false,
                        ImageData = product.ImageData,
                        ImageMimeType = product.ImageMimeType,
                        ImageBase64 = Convert.ToBase64String(product.ImageData),
                        Type = ProductType.Type1,

                    }).ToList()
                },

                TopSellPosts = new TopSellPostsViewModel
                {
                    Title = "",

                    ProductItems = topSellPostProducts.Select(product => new ProductViewModel
                    {
                        SKU = product.SKU,
                        Id = product.Id,
                        ProductName = product.ProductName,
                        Description = "Best dress for everyone ed totam velit risus viverra nobis donec recusandae perspiciatis incididuno",
                        Author = "Admin",
                        Comments = "13",
                        IsIcon = false,
                        IsTitleCenter = false,
                        ImageData = product.ImageData,
                        ImageMimeType = product.ImageMimeType,
                        ImageBase64 = Convert.ToBase64String(product.ImageData),
                        Type = ProductType.Type3,

                    }).ToList()
                }
            };

            viewModel.SummerCollection.IsLast = true;

            return viewModel;
        }
    }
}