namespace SecondHandClothes.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Infrastructure;
    using SecondHandClothes.Models.Sellers;
    using SecondHandClothes.Services.Sellers;

    public class SellersController : Controller
    {
        private readonly ISellerService sellers;

        public SellersController(ISellerService sellers) 
            => this.sellers = sellers;

        [Authorize]
        public IActionResult Become()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeSellerFormModel seller)
        {
            var userId = this.User.Id();

            if (sellers.IsSeller(userId))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(seller);
            }

            this.sellers.Create(
                seller.FirstName,
                seller.LastName,
                seller.PhoneNumber,
                userId);

            return RedirectToAction("Add" ,"Products");
        }
    }
}
