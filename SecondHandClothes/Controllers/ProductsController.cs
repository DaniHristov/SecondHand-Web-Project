namespace SecondHandClothes.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Infrastructure;
    using SecondHandClothes.Models;
    using SecondHandClothes.Models.Products;
    using SecondHandClothes.Services.Products;

    public class ProductsController : Controller
    {
        private readonly SecondHandDbContext data;
        private readonly IProductService products;

        public ProductsController(SecondHandDbContext data, IProductService products)
        {
            this.data = data;
            this.products = products;
        }

        public IActionResult All([FromQuery]AllProductsQueryModel model)
        {
            var products = this.products.All(
                model.Category,
                model.Manufacturer,
                model.SearchTerm,
                model.Sorting,
                model.CurrentPage,
                AllProductsQueryModel.ProductsPerPage);

            var productBrands = this.products.AllProductBrands();
            var productCategories = this.products.AllProductCategories();

            model.TotalProducts = products.TotalProducts;
            model.Products = products.Products;
            model.Manufacturers = productBrands;
            model.Categories = productCategories;
            
            return View(model);
           
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsSeller())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            return this.View(
            new AddProductViewModel
            {
                Categories = this.GetProductsCategories(),
                Sizes = this.GetProductsSizes(),
                Conditions = this.GetProductsConditions(),
                Sexes = this.GetProductsSexes()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddProductViewModel product)
        {
            var sellerId = this.data
                .Sellers
                .Where(s=>s.UserId == this.User.GetId())
                .Select(s=>s.Id)
                .FirstOrDefault();

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (!this.data.Categories.Any(c => c.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!this.data.Sexes.Any(c => c.Id == product.SexId))
            {
                this.ModelState.AddModelError(nameof(product.SexId), "Sex does not exist.");
            }

            if (!this.data.Conditions.Any(c => c.Id == product.ConditionId))
            {
                this.ModelState.AddModelError(nameof(product.ConditionId), "Condition does not exist.");
            }

            if (!this.data.Sizes.Any(c => c.Id == product.SizeId))
            {
                this.ModelState.AddModelError(nameof(product.SizeId), "Size does not exist.");
            }


            if (!ModelState.IsValid)
            {
                product.Categories = this.GetProductsCategories();
                product.Sexes = this.GetProductsSexes();
                product.Conditions = this.GetProductsConditions();
                product.Sizes = this.GetProductsSizes();

                return View(product);
            }

            var productData = new Product
            {
                Title = product.Title,
                Description = product.Description,
                Colour = product.Colour,
                ConditionId = product.ConditionId,
                CategoryId = product.CategoryId,
                SexId = product.SexId,
                Manufacturer = product.Manufacturer,
                Price = product.Price,
                SizeId = product.SizeId,
                ImageURL = product.ImageURL,
                SellerId = sellerId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Products");
        }

        private bool UserIsSeller()
            =>  this.data
                .Sellers
                .Any(s => s.UserId == this.User.GetId());

        private IEnumerable<ProductsCategoryViewModel> GetProductsCategories()
            => this.data
                .Categories
                .Select(c => new ProductsCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.CategoryName
                })
                .ToList();

        private IEnumerable<ProductsSexViewModel> GetProductsSexes()
             => this.data
                .Sexes
                .Select(c => new ProductsSexViewModel
                {
                    Id = c.Id,
                    Name = c.SexType
                })
                .ToList();

        private IEnumerable<ProductsConditionViewModel> GetProductsConditions()
             => this.data
                .Conditions
                .Select(c => new ProductsConditionViewModel
                {
                    Id = c.Id,
                    Name = c.ConditionType
                })
                .ToList();

        private IEnumerable<ProductsSizeViewModel> GetProductsSizes()
             => this.data
                .Sizes
                .Select(c => new ProductsSizeViewModel
                {
                    Id = c.Id,
                    Name = c.SizeType
                })
                .ToList();
    }
}
