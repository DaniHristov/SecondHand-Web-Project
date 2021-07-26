namespace SecondHandClothes.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Models.Home;
    using SecondHandClothes.Services;

    public class HomeController : Controller
    {
        private readonly SecondHandDbContext data;
        private readonly IStatisticsService statistics;

        public HomeController(
            IStatisticsService statistics,
            SecondHandDbContext data)
        {
            this.data = data;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var products = this.data
                .Products
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new ProductIndexViewModel
                {
                    Id = c.Id,
                    Brand = c.Manufacturer,
                    Category = c.Category.CategoryName,
                    ImageUrl = c.ImageURL
                })
                .Take(3)
                .ToList();

            var statistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                Products = products,
                TotalCustomers = statistics.TotalCustomers,
                TotalSellers = statistics.TotalSellers,
                TotalProducts = statistics.TotalProducts
            });
        }

        public IActionResult Error() => View();
    }
}