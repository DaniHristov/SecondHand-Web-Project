namespace SecondHandClothes.Models.Orders
{
    using SecondHandClothes.Data.Models;

    public class OutgoingOrdersViewModel
    {
        public Seller Seller { get; init; }

        public string Town { get; init; }

        public string ShippingAddress { get; init; }

        public decimal Price { get; init; }

        public string CreatedOn { get; init; }

        public string PhoneNumber { get; init; }

        public string ProductId { get; init; }

        public string Status { get; set; }

        public string ImageURL { get; init; }
    }
}
