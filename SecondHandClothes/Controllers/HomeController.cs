namespace SecondHandClothes.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Models.Home;

    public class HomeController : Controller
    {
        private readonly SecondHandDbContext data;

        public HomeController(
            SecondHandDbContext data)
        {
            this.data = data;
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

            var totalUsers = this.data.Users.Count();
            var totalSellers = this.data.Sellers.Count();
            var totalProducts = this.data.Products.Count();

            return View(new IndexViewModel
            {
                Products = products,
                TotalCustomers = totalUsers,
                TotalSellers = totalSellers,
                TotalProducts = totalProducts
            });
        }

        public IActionResult Error() => View();
    }
}