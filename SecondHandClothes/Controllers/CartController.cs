﻿
namespace SecondHandClothes.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Infrastructure;
    using SecondHandClothes.Services.Cart;

    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly SecondHandDbContext data;

        public CartController(ICartService cartService, SecondHandDbContext data)
        {
            this.cartService = cartService;
            this.data = data;
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

            return RedirectToAction("All", "Products");
        }

        [Authorize]
        public IActionResult Delete(string productId, string userId)
        {
            cartService.Delete(productId, userId);

            return RedirectToAction($"MyCart", new {userId = this.User.Id()});
        }
    }
}
