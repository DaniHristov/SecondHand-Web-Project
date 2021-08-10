namespace SecondHandClothes.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Infrastructure;
    using SecondHandClothes.Models.Orders;

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
                    Status = Enum.Parse<OrderStatus>(model.Status.ToString()),
                    ShippingAddress = model.ShippingAddress,
                    ImageURL = imageUrL
                };

                seller.Orders.Add(orderData);
                this.data.Orders.Add(orderData);
                this.data.SaveChanges();
            }

            return RedirectToAction("All", "Products");
        }

        [Authorize]
        public IActionResult MyIncomingOrders()
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
                ShippingAddress = o.ShippingAddress,
                ImageURL = o.ImageURL,
                Status = o.Status.ToString(),
                Id = o.Id

            })
                .ToList();

            return View(orderData);
        }

        [Authorize]
        public IActionResult MyOutgoingOrders()
        {
            var orders = this.data.Orders.Where(o => o.UserId == this.User.Id());

            var orderData = orders.Select(o => new IncomingOrdersViewModel
            {
                FirstName = o.FirstName,
                LastName = o.LastName,
                PhoneNumber = o.PhoneNumber,
                Price = o.Price,
                ShippingAddress = o.ShippingAddress,
                ImageURL = o.ImageURL,
                Status = o.Status.ToString(),
                Id = o.Id

            })
            .ToList();

            return View(orderData);
        }

        [Authorize]
        public IActionResult CompleteOrder(string Id)
        {
            var orderData = this.data.Orders.Where(o => o.Id == Id).FirstOrDefault();

            if (orderData == null)
            {
                return Unauthorized();
            }

            return View(orderData);
        }

        [HttpPost, ActionName("CompleteOrder")]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteOrderConfirmed(string Id)
        {
            var order = this.data.Orders.Find(Id);

            order.Status = OrderStatus.Изпълнена;

            var product =  this.data.Products.Where(o => o.ImageURL == order.ImageURL).FirstOrDefault();

            this.data.Products.Remove(product);
            this.data.SaveChanges();
            return RedirectToAction("MyOrders", "Orders");
        }

        [Authorize]
        public IActionResult DeclineOrder(string Id)
        {
            var orderData = this.data.Orders.Where(o => o.Id == Id).FirstOrDefault();

            if (orderData == null)
            {
                return Unauthorized();
            }

            return View(orderData);
        }

        [HttpPost, ActionName("DeclineOrder")]
        [ValidateAntiForgeryToken]
        public IActionResult DeclineOrderConfirmed(string Id)
        {
            var order = this.data.Orders.Find(Id);

            order.Status = OrderStatus.Отказана;
            this.data.SaveChanges();
            return RedirectToAction("MyOrders", "Orders");
        }
    }
}
