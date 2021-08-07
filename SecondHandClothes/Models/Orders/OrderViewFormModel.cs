namespace SecondHandClothes.Models.Orders
{
    using SecondHandClothes.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class OrderViewFormModel
    {
        [Required]
        public string FirstName { get; init; }

        [Required]
        public string LastName { get; init; }

        [Required]
        public string PhoneNumber { get; init; }

        [Required]
        public string ShippingAddress { get; init; }

        [Required]
        public string Note { get; init; }

        public OrderStatus Status { get; init; }

    }
}
