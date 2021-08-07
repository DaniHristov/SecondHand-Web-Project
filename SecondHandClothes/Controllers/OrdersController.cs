namespace SecondHandClothes.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Infrastructure;
    using SecondHandClothes.Models.Orders;
    using System.Collections.Generic;
    using System.Linq;

    public class OrdersController : Controller
    {
        private readonly SecondHandDbContext data;

        public OrdersController(SecondHandDbContext data)
        {
            this.data = data;
        }

        [HttpGet]
        [Authorize]
        public IActionResult MakeOrder()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult MakeOrder(OrderViewFormModel model)
        {
            var products = this.data.CartItems.Where(ci => ci.UserId == this.User.Id()).ToList();

            foreach (var product in products.ToList())
            {
                var productData = this.data.Products.Where(p => p.Id == product.ProductId).FirstOrDefault();
                var sellerId = productData.SellerId;
                var imageUrL = productData.ImageURL;

                var current = this.data.Orders.Where(x => x.SellerId == sellerId).FirstOrDefault();
                var seller = this.data.Sellers.Find(sellerId);

                var orderData = new Order
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Products = products,
                    UserId = this.User.Id(),
                    Note = model.Note,
                    SellerId = sellerId,
                    Price = product.Product.Price,
                    ShippingAddress = model.ShippingAddress,
                    Status = OrderStatus.Pending,
                    ImageURL = imageUrL,
                };

                seller.Orders.Add(orderData);
                this.data.Orders.Add(orderData);
                this.data.SaveChanges();
            }

            return RedirectToAction("All", "Products");
        }

        public IActionResult MyOrders()
        {
            var seller = this.data
                    .Sellers
                    .FirstOrDefault(s => s.UserId == this.User.Id());

            var orders = this.data.Orders.OrderByDescending(x => x).Where(o => o.SellerId == seller.Id);

            var orderData = orders.Select(o => new IncomingOrdersViewModel
            {
                FirstName = o.FirstName,
                LastName = o.LastName,
                PhoneNumber = o.PhoneNumber,
                Price = o.Price,
                ShippinhAddress = o.ShippingAddress,
                ImageURL = o.ImageURL

            })
                .ToList();

            return View(orderData);

        }
    }

}
