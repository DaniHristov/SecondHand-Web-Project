namespace SecondHandClothes.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using SecondHandClothes.Data;
    using SecondHandClothes.Models;

    public class ProductService : IProductService
    {
        private readonly SecondHandDbContext data;


        public ProductService(SecondHandDbContext data)
        {
            this.data = data;
        }

        public ProductQueryServiceModel All(
            string category,
            string manufacturer,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage)
        {

            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                productsQuery = productsQuery
                    .Where(c => c.Category.CategoryName == category);
            }

            if (!string.IsNullOrWhiteSpace(manufacturer))
            {
                productsQuery = productsQuery.Where(c =>
                     c.Manufacturer == manufacturer);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    (p.Title).ToLower().Contains(searchTerm.ToLower()) ||
                    p.Category.CategoryName.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.PriceAscending => productsQuery.OrderBy(x => x.Price),
                ProductSorting.PriceDescending => productsQuery.OrderByDescending(x => x.Price),
                ProductSorting.CreatedOn or _ => productsQuery.OrderByDescending(x => x.CreatedOn)
            };

            var productsCount = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Brand = p.Manufacturer,
                    Condition = p.Condition.ConditionType,
                    Price = p.Price,
                    ImageUrl = p.ImageURL,
                    Title = p.Title
                })
                .ToList();

            return new ProductQueryServiceModel
            {
                TotalProducts = productsCount,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                Products = products
            };
        }

        public IEnumerable<string> AllProductBrands()
            => this.data.Products
                .Select(p => p.Manufacturer)
                .Distinct()
                .OrderBy(p=>p)
                .ToList();

        public IEnumerable<string> AllProductCategories()
            =>this.data.Products
                    .Select(p => p.Category.CategoryName)
                    .Distinct()
                    .OrderBy(x=>x)
                    .ToList();
    }
}
