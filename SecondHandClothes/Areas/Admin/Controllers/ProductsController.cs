using Microsoft.AspNetCore.Mvc;
using SecondHandClothes.Services.Products;

namespace SecondHandClothes.Areas.Admin.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IProductService products;

        public ProductsController(IProductService products) => this.products = products;

        public IActionResult All()
        {
            var products = this.products
                .All(publicOnly: false)
                .Products;

            return View(products);
        }

        public IActionResult ChangeVisibility(string id)
        {
            this.products.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
