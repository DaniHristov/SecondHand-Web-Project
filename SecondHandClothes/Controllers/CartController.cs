namespace SecondHandClothes.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Infrastructure;
    using SecondHandClothes.Services.Cart;

    using static WebConstants;

    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [Authorize]
        public IActionResult MyCart(string userId)
        {
            if (this.User.Id() != userId || User.IsAdmin())
            {
                return Unauthorized();
            }

            var usersProducts = cartService.UsersCart(userId);

            return View(usersProducts);
        }

        [Authorize]
        public IActionResult AddToCart(string productId, string userId)
        {

            if (this.User.Id() != userId || User.IsAdmin())
            {
                return Unauthorized();
            }

            cartService.AddProductToCart(productId, userId);

            TempData[GlobalMessageKey] = "Продуктът беше успешно добавен във вашата количка!";

            return RedirectToAction("All", "Products");
        }

        [Authorize]
        public IActionResult Delete(string productId, string userId)
        {
            cartService.Delete(productId, userId);

            TempData[GlobalMessageKey] = "Продуктът беше успешно премахнат от вашата количка!";

            return RedirectToAction($"MyCart", new {userId = this.User.Id()});
        }
    }
}
