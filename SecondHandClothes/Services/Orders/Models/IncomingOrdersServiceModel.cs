namespace SecondHandClothes.Services.Orders.Models
{
    public class IncomingOrdersServiceModel
    {
        public string Id { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string PhoneNumber { get; init; }

        public string ProductId { get; init; }

        public string CreatedOn { get; init; }

        public string Town { get; set; }

        public string ShippingAddress { get; init; }

        public string ImageURL { get; init; }

        public decimal Price { get; init; }

        public string Status { get; set; }

    }
}
