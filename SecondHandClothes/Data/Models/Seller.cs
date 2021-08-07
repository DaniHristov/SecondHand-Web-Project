namespace SecondHandClothes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    
    public class Seller
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(SellerNameMaxLength)]
        public string FirstName { get; init; }

        [MaxLength(SellerNameMaxLength)]
        [Required]
        public string LastName { get; init; }

        [Required]
        [MaxLength(SellerPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; init; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
