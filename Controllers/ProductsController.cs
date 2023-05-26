using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly ProductContext _productContext;

        public ProductsController(ProductService productService, ProductContext productContext)
        {
            _productService = productService;
            _productContext = productContext;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();

            var viewModel = new ProductsIndexViewModel
            {
                ProductItems = products.ToList(),
            };
            return View(viewModel);
        }

        public IActionResult Register()
        {
            var viewModel = new ProductRegistrationViewModel
            {
                Tags = _productContext.Tags.Select(t => t.TagName).ToList()
            };

            return View(viewModel);
        }

        // HttpPost to register a new product
        [HttpPost]
        [Authorize(Roles = "Sysadmin")]
        public async Task<IActionResult> Register(ProductRegistrationViewModel productRegistrationViewModel, IFormFile image, List<string> tags)
        {
            if (ModelState.IsValid)
            {

                if (productRegistrationViewModel.Image != null)
                {
                    using (var binaryReader = new BinaryReader(productRegistrationViewModel.Image.OpenReadStream()))
                    {
                        byte[] imageData = binaryReader.ReadBytes((int)productRegistrationViewModel.Image.Length);
                        productRegistrationViewModel.ProductImage = imageData;
                    }

                    productRegistrationViewModel.Tags = tags;

                    bool result = await _productService.CreateProductAsync(productRegistrationViewModel, image);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "There was an error creating the product");
                    }
                }
            }

            productRegistrationViewModel.Tags ??= _productContext.Tags.Select(t => t.TagName).ToList();

            return View(productRegistrationViewModel);
        }

        // Httpget to display a product when clicking on the "edit"-link from products
        [HttpGet]
        [Route("products/edit/{sku}")]
        [Authorize(Roles = "Sysadmin")]
        public async Task<IActionResult> Edit(string sku)
        {
            var product = await _productService.GetOneProductBySKUAsync(sku);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductEditViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                SKU = product.SKU,
                DiscountPrice = product.Price.DiscountPrice,
                OrdinaryPrice = product.Price.OrdinaryPrice,
                OriginalPrice = product.Price.OriginalPrice,
                Category = product.Category.Category,
                Tags = _productContext.Tags.Select(t => t.TagName).ToList(),
                SelectedTags = product.Tags.ToList()
            };

            return View(viewModel);
        }

        // HttpPost for editing the product
        [HttpPost]
        [Route("products/edit/{sku}")]
        [Authorize(Roles = "Sysadmin")]
        public async Task<IActionResult> Edit(string sku, ProductEditViewModel model, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                bool result = await _productService.EditProductAsync(sku, model, image);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "There was an error editing the product");
                }
            }

            return View(model);
        }

        // Used on the product detail page to display the right product.
        // If more time: build the method to also work with adding "additional products" based on the TagId instead of Category.
        public async Task<IActionResult> Details(string sku)
        {
            if (!string.IsNullOrEmpty(sku))
            {
                var products = await _productService.GetProductDetailsBySkuAsync(sku);

                if (products == null)
                {
                    return NotFound();
                }

                var viewModel = new ProductDetailsViewModel
                {
                    ProductItems = products.ToList(),
                };

                foreach (var product in viewModel.ProductItems)
                {
                    if (!string.IsNullOrEmpty(product.CategoryName))
                    {
                        var relatedProducts = await _productService.GetByCategoryAsync(product.CategoryName);
                        product.RelatedProducts = relatedProducts.Take(4).ToList();
                    }
                }

                return View(viewModel);
            }

            return RedirectToAction("Index", "Products");
        }
    }
}