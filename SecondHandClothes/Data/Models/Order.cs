namespace SecondHandClothes.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string PhoneNumber { get; init; }

        public string Town { get; init; }

        public string ShippingAddress { get; init; }
        
        public string ProductId { get; init; }

        public string UserId { get; init; }

        public User User { get; init; }

        public int SellerId { get; init; }

        public decimal Price { get; init; }

        public string Note { get; init; }

        public string ImageURL { get; init; }

        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        public string ProductTitle { get; init; }

        public OrderStatus Status { get; set; }

        public ICollection<Item> Products { get; init; } = new List<Item>();

        public ICollection<Seller> Sellers { get; init; } = new List<Seller>();

    }
}
