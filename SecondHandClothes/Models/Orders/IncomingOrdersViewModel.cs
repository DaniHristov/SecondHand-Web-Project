using SecondHandClothes.Data.Models;

namespace SecondHandClothes.Models.Orders
{
    public class IncomingOrdersViewModel
    {
        public string Id { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string PhoneNumber { get; init; }

        public string ShippingAddress { get; init; }

        public string ImageURL { get; init; }
        
        public decimal Price { get; init; }

        public string Status { get; set; }

    }
}
