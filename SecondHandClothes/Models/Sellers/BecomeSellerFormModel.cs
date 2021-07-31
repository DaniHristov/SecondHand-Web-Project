namespace SecondHandClothes.Models.Sellers
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class BecomeSellerFormModel
    { 
        [Required]
        [StringLength(SellerNameMaxLength,MinimumLength = SellerNameMinLength)]
        [Display(Name = "Име*")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(SellerNameMaxLength, MinimumLength = SellerNameMinLength , ErrorMessage = "")]
        [Display(Name = "Фамилия*")]
        public string LastName { get; set; }

        [Required]
        [StringLength(SellerPhoneNumberMaxLength, MinimumLength = SellerPhoneNumberMinLength)]
        [Display(Name = "Телефон*")]
        public string PhoneNumber { get; set; }


    }
}
