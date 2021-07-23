using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondHandClothes.Data;
using SecondHandClothes.Data.Models;
using SecondHandClothes.Models.Products;
using SecondHandClothes.Models.Products.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandClothes.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SecondHandDbContext data;

        public ProductsController(SecondHandDbContext data)
        {
            this.data = data;
        }

        public IActionResult All([FromQuery]AllProductsQueryModel model)
        {
            var productsQuery = this.data.Products.AsQueryable();
            //TODO: Some Validations

            if (!string.IsNullOrWhiteSpace(model.Category))
            {
                productsQuery = productsQuery
                    .Where(c => c.Category.CategoryName == model.Category);
            }

            if (!string.IsNullOrWhiteSpace(model.Manufacturer))
            {
                productsQuery = productsQuery.Where(c =>
                     c.Manufacturer == model.Manufacturer);
            }

            if (!string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    (p.Title).ToLower().Contains(model.SearchTerm.ToLower()) ||
                    p.Category.CategoryName.ToLower().Contains(model.SearchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(model.SearchTerm.ToLower()));
            }

            var productBrands = this.data.Products
                .Select(p => p.Manufacturer)
                .Distinct()
                .ToList();

            var productCategories = this.data.Products
                .Select(p => p.Category.CategoryName)
                .Distinct()
                .ToList();

            productsQuery = model.Sorting switch
            {
                ProductSorting.PriceAscending => productsQuery.OrderBy(x => x.Price),
                ProductSorting.PriceDescending => productsQuery.OrderByDescending(x => x.Price),
                ProductSorting.CreatedOn or _ => productsQuery.OrderByDescending(x => x.Id)
            };

            var productsCount = productsQuery.Count(); 

            var products = productsQuery
                .Skip((model.CurrentPage - 1) * AllProductsQueryModel.ProductsPerPage)
                .Take(AllProductsQueryModel.ProductsPerPage)
                .Select(p => new ListingProductsViewModel
                {
                    Id = p.Id,
                    Brand = p.Manufacturer,
                    Condition = p.Condition.ConditionType,
                    Price = p.Price,
                    ImageUrl = p.ImageURL,
                    Title = p.Title
                })
                .ToList();

            model.TotalProducts = productsCount;
            model.Products = products;
            model.Manufacturers = productBrands;
            model.Categories = productCategories;
            
            return View(model);
           
        }

        [Authorize]
        public IActionResult Add()
            => this.View(
                new AddProductViewModel { 
                    Categories = this.GetProductsCategories(), 
                    Sizes = this.GetProductsSizes(),
                    Conditions = this.GetProductsConditions(), 
                    Sexes = this.GetProductsSexes() });

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddProductViewModel product)
        {
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
                ImageURL = product.ImageURL
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Products");
        }

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
