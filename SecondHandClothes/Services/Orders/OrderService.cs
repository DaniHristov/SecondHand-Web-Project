namespace SecondHandClothes.Services.Orders
{
    using System.Linq;
    using System.Collections.Generic;
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using System;
    using SecondHandClothes.Services.Sellers;
    using SecondHandClothes.Services.Orders.Models;

    public class OrderService : IOrderService
    {
        private readonly SecondHandDbContext data;
        private readonly ISellerService sellers;

        public OrderService(SecondHandDbContext data)
        {
            this.data = data;
        }


        public OrderService(ISellerService sellers, SecondHandDbContext data)
        {
            this.data = data;
            this.sellers = sellers;
        }

        public string CreateOrder(string firstName,
            string lastName,
            string phoneNumber,
            IList<Item> products,
            string userId,
            string note,
            int sellerId,
            string productId,
            string productTitle,
            decimal price,
            OrderStatus status,
            string shippingAddress,
            string imgURL,
            string town)
        {
            var orderData = new Order
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Products = products,
                UserId = userId,
                Note = note,
                SellerId = sellerId,
                ProductId = productId,
                ProductTitle = productTitle,
                Price = price,
                Status = Enum.Parse<OrderStatus>(status.ToString()),
                ShippingAddress = shippingAddress,
                ImageURL = imgURL,
                Town = town
            };
            this.data.Orders.Add(orderData);
            this.data.SaveChanges();

            return orderData.Id;
        }

        public IList<IncomingOrdersServiceModel> GetIncomingOrders(int sellerId)
            => this.data.Orders
            .Where(o => o.SellerId == sellerId)
            .OrderByDescending(o => o.CreatedOn)
            .Select(o => new IncomingOrdersServiceModel
            {
                FirstName = o.FirstName,
                LastName = o.LastName,
                CreatedOn = o.CreatedOn.ToString("d"),
                Id = o.Id,
                ImageURL = o.ImageURL,
                PhoneNumber = o.PhoneNumber,
                Price = o.Price,
                ProductId = o.ProductId,
                ShippingAddress = o.ShippingAddress,
                Status = o.Status.ToString(),
                Town = o.Town
            })
                .ToList();

        public IList<OutgoingOrdersServiceModel> GetOutgoingOrders(Seller seller, string userId)
            => this.data.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedOn)
            .Select(o => new OutgoingOrdersServiceModel
            {
                CreatedOn = o.CreatedOn.ToString("d"),
                ImageURL = o.ImageURL,
                PhoneNumber = o.PhoneNumber,
                Price = o.Price,
                ProductId = o.ProductId,
                Seller = seller,
                ShippingAddress = o.ShippingAddress,
                Status = o.Status.ToString(),
                Town = o.Town
            })
            .ToList();

        public List<Item> GetItems(string userId)
            => this.data.CartItems.Where(ci => ci.UserId == userId).ToList();

        public Order GetOrderById(string orderId)
            => this.data.Orders.Where(o => o.Id == orderId).FirstOrDefault();

        public IQueryable<Order> GetOrdersBySeller(int sellerId)
            => this.data.Orders.OrderByDescending(x => x.CreatedOn).Where(o => o.SellerId == sellerId);



        public Product GetProductData(string productId)
            => this.data.Products.Where(p => p.Id == productId).FirstOrDefault();

        public void CompleteOrder(Product product,Order order)
        {
            order.Status = OrderStatus.Изпълнена;
            this.data.Products.Remove(product);
            this.data.SaveChanges();
        }

        public void DeclineOrder(Order order)
        {
            order.Status = OrderStatus.Отказана;
            this.data.SaveChanges();
        }

        public bool IsAlreadyOrdered(string userId, string productId)
        {
            var isOrdered = this.data.Orders.Where(o => o.UserId == userId && o.ProductId == productId).FirstOrDefault();

            if (isOrdered==null)
            {
                return false;
            }

            return true;
        }
    }
}
