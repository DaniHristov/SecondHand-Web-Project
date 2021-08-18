namespace SecondHandClothes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Order
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; init; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; init; }

        [MaxLength(OrderTownMaxLength)]
        public string Town { get; init; }

        [Required]
        [MaxLength(OrderShippingAddressMaxLength)]
        public string ShippingAddress { get; init; }

        public string ProductId { get; init; }

        [Required]
        public string UserId { get; init; }
        public User User { get; init; }

        public int SellerId { get; init; }

        public decimal Price { get; init; }

        [MaxLength(OrderNoteMaxLength)]
        public string Note { get; init; }

        [Required]
        public string ImageURL { get; init; }

        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        [Required]
        public string ProductTitle { get; init; }

        public OrderStatus Status { get; set; }

        public ICollection<Item> Products { get; init; } = new List<Item>();

        public ICollection<Seller> Sellers { get; init; } = new List<Seller>();

    }
}
