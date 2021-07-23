using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondHandClothes.Data;
using SecondHandClothes.Data.Models;
using SecondHandClothes.Infrastructure;
using SecondHandClothes.Models.Sellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandClothes.Controllers
{
    public class SellersController : Controller
    {
        private readonly SecondHandDbContext data;

        public SellersController(SecondHandDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Become()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeSellerFormModel seller)
        {
            var userId = this.User.GetId();

            var userIsAlreadySeller = this.data
                .Sellers
                .Any(x => x.UserId == userId);

            if (userIsAlreadySeller)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(seller);
            }

            var sellerData = new Seller
            {
                FirstName = seller.FirstName,
                LastName = seller.LastName,
                PhoneNumber = seller.PhoneNumber,
                UserId = userId
            };

            this.data.Sellers.Add(sellerData);
            this.data.SaveChanges();

            return RedirectToAction("Add" ,"Products");
        }
    }
}
