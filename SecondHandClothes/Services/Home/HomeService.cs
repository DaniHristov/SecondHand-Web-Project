namespace SecondHandClothes.Services.Home
{
    using SecondHandClothes.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class HomeService : IHomeService
    {
        private readonly SecondHandDbContext data;

        public HomeService(SecondHandDbContext data)
        {
            this.data = data;
        }

        public IList<IndexQueryServiceModel> GetIndexProducts()
        {
            var products = this.data
                .Products
                .Where(p=>p.IsPublic)
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new IndexQueryServiceModel
                {
                    Id = c.Id,
                    Brand = c.Manufacturer,
                    Category = c.Category.CategoryName,
                    ImageUrl = c.ImageURL,
                    Price = c.Price,
                    Title = c.Title
                })
                .Take(3)
                .ToList();

            return products;
        }
    }
}
