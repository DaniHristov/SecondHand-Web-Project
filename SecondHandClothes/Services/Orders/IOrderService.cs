namespace SecondHandClothes.Services.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Services.Orders.Models;

    public interface IOrderService
    {

        public bool IsAlreadyOrdered(string userId, string productId);

        public void CompleteOrder(Product product,Order order);

        public void DeclineOrder(Order order);

        public List<Item> GetItems(string userId);

        public Product GetProductData(string productId);

        public Order GetOrderById(string orderId);

        public IQueryable<Order> GetOrdersBySeller(int sellerId);

        public IList<IncomingOrdersServiceModel> GetIncomingOrders(int sellerId);

        public IList<OutgoingOrdersServiceModel> GetOutgoingOrders(Seller seller ,string userId);


        public string CreateOrder(
            string firstName,
            string lastName,
            string PhoneNumber, 
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
            string town);
    }
}
