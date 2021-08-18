namespace SecondHandClothes.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using SecondHandClothes.Models.Home;
    using SecondHandClothes.Services;
    using SecondHandClothes.Services.Home;
    using static WebConstants.Cache;
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IHomeService home;
        private readonly IMemoryCache cache;


        public HomeController(IStatisticsService statistics, IHomeService home, IMemoryCache cache)
        {
            this.statistics = statistics;
            this.home = home;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var latestProducts = this.cache.Get<IList<IndexQueryServiceModel>>(LatestProductsCacheKey);

            if (latestProducts == null)
            {
                latestProducts = home.GetIndexProducts();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(50));

                this.cache.Set(LatestProductsCacheKey, latestProducts, cacheOptions);
            }


            var statistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                Products = latestProducts,
                TotalCustomers = statistics.TotalCustomers,
                TotalSellers = statistics.TotalSellers,
                TotalProducts = statistics.TotalProducts
            });
        }

        public IActionResult Error() => View();
    }
}