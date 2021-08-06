namespace SecondHandClothes.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Models.Home;
    using SecondHandClothes.Services;
    using SecondHandClothes.Services.Home;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IHomeService home;


        public HomeController(IStatisticsService statistics, IHomeService home)
        {
            this.statistics = statistics;
            this.home = home;
        }

        public IActionResult Index(ProductIndexViewModel model)
        {
            var products = home.GetIndexProducts(
                model.Id, model.Brand, model.Category, model.ImageUrl);

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