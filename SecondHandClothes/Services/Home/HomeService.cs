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

        public IEnumerable<IndexQueryServiceModel> GetIndexProducts(string id, string brand, string categoryName, string imageUrl)
        {
            var products = this.data
                .Products
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new IndexQueryServiceModel
                {
                    Id = c.Id,
                    Brand = c.Manufacturer,
                    Category = c.Category.CategoryName,
                    ImageUrl = c.ImageURL
                })
                .Take(3)
                .ToList();

            return products;
        }
    }
}
