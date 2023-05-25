using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.Models.Entities;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class ProductService
    {
        private readonly ProductContext _productContext;

        public ProductService(ProductContext productContext)
        {
            _productContext = productContext;
        }

        // Method to create a new product
        public async Task<bool> CreateProductAsync(ProductRegistrationViewModel productRegistrationViewModel, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return false;
            }

            using var stream = new MemoryStream();
            await image.CopyToAsync(stream);

            // Read price
            var ordinaryPrice = productRegistrationViewModel.OrdinaryPrice;
            var discountPrice = productRegistrationViewModel.DiscountPrice;
            var originalPrice = productRegistrationViewModel.OriginalPrice;

            var price = new PriceEntity(
                ordinaryPrice: ordinaryPrice,
                originalPrice: originalPrice,
                discountPrice: discountPrice
            );

            var categoryEntity = new ProductCategoryEntity
            {
                Category = productRegistrationViewModel.Category
            };

            var productEntity = new ProductEntity
            {
                ProductName = productRegistrationViewModel.ProductName,
                Price = price,
                ImageData = stream.ToArray(),
                ImageMimeType = image.ContentType,
                Category = categoryEntity,
            };

            var selectedTags = await _productContext.Tags
            .Where(t => productRegistrationViewModel.Tags.Contains(t.TagName))
            .ToListAsync();

            foreach (var tag in selectedTags)
            {
                var productTagEntity = new ProductTagEntity
                {
                    Product = productEntity,
                    Tag = tag
                };

                _productContext.ProductTags.Add(productTagEntity);
            }

            _productContext.ProductItems.Add(productEntity);
            await _productContext.SaveChangesAsync();

            // Assign the Id to the product
            var productId = productEntity.Id;

            // Update the SKU using ID and productName
            productEntity.SKU = $"{productId}-{productEntity.ProductName.Replace(" ", "-")}".ToLowerInvariant();

            await _productContext.SaveChangesAsync();

            return true;
        }

        // Method to return all products
        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var products = new List<ProductModel>();

            var items = await _productContext.ProductItems
                .Include(x => x.Price)
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .ToListAsync();

            foreach (var item in items)
            {
                var productModel = new ProductModel
                {
                    SKU = item.SKU,
                    Id = (int)item.Id,
                    ProductName = item.ProductName,
                    Price = new PriceEntity(
                        ordinaryPrice: item.Price.OrdinaryPrice,
                        originalPrice: item.Price.OriginalPrice,
                        discountPrice: item.Price.DiscountPrice
                    )
                    {
                        IsOriginalPrice = item.Price.IsOriginalPrice,
                        IsDiscountPrice = item.Price.IsDiscountPrice,
                        IsOrdinaryPrice = item.Price.IsOrdinaryPrice
                    },
                    ImageData = item.ImageData,
                    ImageMimeType = item.ImageMimeType,
                    ImageBase64 = Convert.ToBase64String(item.ImageData),
                    Category = item.Category,
                    CategoryName = item.Category?.Category,
                };

                if (productModel.Price.IsOriginalPrice || productModel.Price.IsDiscountPrice)
                {
                    if (productModel.Price.IsOriginalPrice)
                    {
                        productModel.Price.IsDiscountPrice = false;
                        productModel.Price.IsOrdinaryPrice = false;
                    }
                    else if (productModel.Price.IsDiscountPrice)
                    {
                        productModel.Price.IsOriginalPrice = false;
                        productModel.Price.IsOrdinaryPrice = false;
                    }
                }
                else
                {
                    productModel.Price.IsOrdinaryPrice = true;
                }

                products.Add(productModel);
            }

            return products;
        }

        // Method to get more then one product based on SKU
        public async Task<IEnumerable<ProductModel>> GetProductDetailsBySkuAsync(string sku)
        {
            var products = new List<ProductModel>();

            var item = await _productContext.ProductItems
                .Include(x => x.Price)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.SKU == sku);

            if (item != null)
            {
                var productModel = new ProductModel
                {
                    SKU = item.SKU,
                    Id = (int)item.Id,
                    ProductName = item.ProductName,
                    Price = new PriceEntity(
                        ordinaryPrice: item.Price.OrdinaryPrice,
                        originalPrice: item.Price.OriginalPrice,
                        discountPrice: item.Price.DiscountPrice
                    )
                    {
                        IsOriginalPrice = item.Price.IsOriginalPrice,
                        IsDiscountPrice = item.Price.IsDiscountPrice,
                        IsOrdinaryPrice = item.Price.IsOrdinaryPrice
                    },
                    ImageData = item.ImageData,
                    ImageMimeType = item.ImageMimeType,
                    ImageBase64 = Convert.ToBase64String(item.ImageData),
                    Category = item.Category,
                    CategoryName = item.Category?.Category,
                };

                if (productModel.Price.IsOriginalPrice || productModel.Price.IsDiscountPrice)
                {
                    if (productModel.Price.IsOriginalPrice)
                    {
                        productModel.Price.IsDiscountPrice = false;
                        productModel.Price.IsOrdinaryPrice = false;
                    }
                    else if (productModel.Price.IsDiscountPrice)
                    {
                        productModel.Price.IsOriginalPrice = false;
                        productModel.Price.IsOrdinaryPrice = false;
                    }
                }
                else
                {
                    productModel.Price.IsOrdinaryPrice = true;
                }

                products.Add(productModel);
            }

            return products;
        }

        // Method to get one product based on SKU
        public async Task<ProductModel> GetOneProductBySKUAsync(string sku)
        {
            var item = await _productContext.ProductItems
                .Include(x => x.Price)
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.SKU == sku);

            if (item != null)
            {
                var productModel = new ProductModel
                {
                    SKU = item.SKU,
                    Id = (int)item.Id,
                    ProductName = item.ProductName,
                    Price = new PriceEntity(
                        ordinaryPrice: item.Price.OrdinaryPrice,
                        originalPrice: item.Price.OriginalPrice,
                        discountPrice: item.Price.DiscountPrice
                    )
                    {
                        IsOriginalPrice = item.Price.IsOriginalPrice,
                        IsDiscountPrice = item.Price.IsDiscountPrice,
                        IsOrdinaryPrice = item.Price.IsOrdinaryPrice
                    },
                    ImageData = item.ImageData,
                    ImageMimeType = item.ImageMimeType,
                    ImageBase64 = Convert.ToBase64String(item.ImageData),
                    Category = item.Category,
                    CategoryName = item.Category?.Category,
                };

                if (productModel.Price.IsOriginalPrice || productModel.Price.IsDiscountPrice)
                {
                    if (productModel.Price.IsOriginalPrice)
                    {
                        productModel.Price.IsDiscountPrice = false;
                        productModel.Price.IsOrdinaryPrice = false;
                    }
                    else if (productModel.Price.IsDiscountPrice)
                    {
                        productModel.Price.IsOriginalPrice = false;
                        productModel.Price.IsOrdinaryPrice = false;
                    }
                }
                else
                {
                    productModel.Price.IsOrdinaryPrice = true;
                }

                return productModel;
            }

            return null;
        }

        // Method to get a product when clicking on the edit link from the product view. To display that specific product
        // Only available as a "sysadmin"
        public async Task<bool> EditProductAsync(string sku, ProductEditViewModel productEditViewModel, IFormFile? image)
        {
            var productEntity = await _productContext.ProductItems
                .Include(p => p.Category)
                .Include(p => p.Price)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(x => x.SKU == sku);

            if (productEntity == null)
            {
                return false;
            }

            // Update only the fields where the data is changed, else keep the original data
            if (!string.IsNullOrEmpty(productEditViewModel.ProductName))
            {
                productEntity.ProductName = productEditViewModel.ProductName;
            }

            if (!string.IsNullOrEmpty(productEditViewModel.Category))
            {
                productEntity.Category.Category = productEditViewModel.Category;
            }

            if (productEditViewModel.OrdinaryPrice.HasValue)
            {
                productEntity.Price.OrdinaryPrice = productEditViewModel.OrdinaryPrice.Value;
            }

            if (productEditViewModel.DiscountPrice.HasValue)
            {
                productEntity.Price.DiscountPrice = productEditViewModel.DiscountPrice.Value;
            }

            if (productEditViewModel.OriginalPrice.HasValue)
            {
                productEntity.Price.OriginalPrice = productEditViewModel.OriginalPrice.Value;
            }

            // Update tags
            var selectedTags = productEditViewModel.Tags ?? new List<string>();
            var existingTags = await _productContext.Tags.ToListAsync();
            var productTags = await _productContext.ProductTags
                .Where(pt => pt.Product.SKU == sku)
                .ToListAsync();

            foreach (var tag in existingTags)
            {
                if (selectedTags.Contains(tag.TagName))
                {
                    // Add new tag
                    if (!productTags.Any(pt => pt.TagId == tag.Id))
                    {
                        productTags.Add(new ProductTagEntity { ProductId = productEntity.Id, TagId = tag.Id });
                    }
                }
                else
                {
                    // Remove existing tag
                    var productTag = productTags.FirstOrDefault(pt => pt.TagId == tag.Id);
                    if (productTag != null)
                    {
                        _productContext.ProductTags.Remove(productTag);
                    }
                }
            }

            if (image != null && image.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await image.CopyToAsync(stream);
                    productEntity.ImageData = stream.ToArray();
                    productEntity.ImageMimeType = image.ContentType;
                }
            }

            await _productContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ProductModel>> GetByCategoryAsync(string categoryName)
        {
            var relatedProducts = await _productContext.ProductCategories
                .Include(pc => pc.Product)
                .ThenInclude(p => p.Price)
                .Include(pc => pc.Product)
                .ThenInclude(p => p.Category)
                .Where(pc => pc.Category == categoryName && pc.Product.SKU != null)
                .Select(pc => pc.Product)
                .ToListAsync();

            var products = relatedProducts.Select(product => new ProductModel
            {
                ProductName = product.ProductName,
                ImageData = product.ImageData,
                ImageMimeType = product.ImageMimeType,
                RelatedProducts = relatedProducts
                    .Where(p => p.Id != product.Id)
                    .Select(p => new ProductModel
                    {
                        ImageData = p.ImageData,
                        ImageMimeType = p.ImageMimeType,
                        ProductName = p.ProductName
                    })
                    .ToList()
            }).ToList();

            return products;
        }
    }
}