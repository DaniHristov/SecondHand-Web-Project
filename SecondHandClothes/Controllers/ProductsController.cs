using Microsoft.AspNetCore.Mvc;
using SecondHandClothes.Data;
using SecondHandClothes.Data.Models;
using SecondHandClothes.Models.Products;
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

        public IActionResult Add()
            => this.View(
                new AddProductViewModel { 
                    Categories = this.GetProductsCategories(), 
                    Sizes = this.GetProductsSizes(),
                    Conditions = this.GetProductsConditions(), 
                    Sexes = this.GetProductsSexes() });

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
                Model = product.Model,
                Price = product.Price,
                PhoneNumber = product.PhoneNumber,
                SizeId = product.SizeId,
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
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
