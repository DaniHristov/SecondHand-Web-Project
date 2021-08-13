namespace SecondHandClothes.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Infrastructure;
    using SecondHandClothes.Models.Orders;
    using SecondHandClothes.Services.Orders;
    using SecondHandClothes.Services.Products;
    using SecondHandClothes.Services.Sellers;
    using System.Linq;

    public class OrdersController : Controller
    {
        private readonly IOrderService ordersService;
        private readonly ISellerService sellersService;
        private readonly IProductService productsService;

        public OrdersController(IOrderService orders, ISellerService sellers, IProductService productsService)
        {
            this.ordersService = orders;
            this.sellersService = sellers;
            this.productsService = productsService;
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
            var userId = this.User.Id();
            var products = ordersService.GetItems(userId);

            if (products.Count == 0)
            {
                return View("Error", BadRequest());
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            foreach (var product in products.ToList())
            {
                var productData = ordersService.GetProductData(product.ProductId);
                var sellerId = productData.SellerId;
                var imageURL = productData.ImageURL;
                var productTitle = productData.Title;
                var productId = productData.Id;
                var price = product.Product.Price;
                var seller = sellersService.GetProductSellerById(sellerId);

                var orderId = ordersService.CreateOrder(model.FirstName, model.LastName, model.PhoneNumber, products, userId, model.Note, sellerId, productId, productTitle, price, model.Status, model.ShippingAddress, imageURL, model.Town);

                var order = ordersService.GetOrderById(orderId);
                seller.Orders.Add(order);
            }

            return RedirectToAction("All", "Products");
        }

        [Authorize]
        public IActionResult MyIncomingOrders()
        {
            var userId = this.User.Id();
            var sellerId = sellersService.SellerId(userId);

            var orderData = ordersService.GetIncomingOrders(sellerId);

            return View(orderData);
        }

        [Authorize]
        public IActionResult MyOutgoingOrders()
        {
            var userId = this.User.Id();
            var seller = sellersService.GetSellerByUserId(this.User.Id());

            if (seller == null)
            {
                return NotFound();
            }

            var orderData = ordersService.GetOutgoingOrders(seller, userId);

            return View(orderData);
        }

        [Authorize]
        public IActionResult CompleteOrder(string Id)
        {
            var orderData = ordersService.GetOrderById(Id);

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
            var order = ordersService.GetOrderById(Id);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = OrderStatus.Изпълнена;

            var product = productsService.ProductByOrderId(order.ProductId);

            this.ordersService.CompleteOrder(product,order);

            return RedirectToAction("MyIncomingOrders", "Orders");
        }

        [Authorize]
        public IActionResult DeclineOrder(string Id)
        {
            var orderData = ordersService.GetOrderById(Id);

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
            var order = ordersService.GetOrderById(Id);

            ordersService.DeclineOrder(order);

            return RedirectToAction("MyOutgoingOrders", "Orders");
        }
    }
}
