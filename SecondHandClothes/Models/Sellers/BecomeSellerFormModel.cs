namespace SecondHandClothes.Models.Sellers
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class BecomeSellerFormModel
    {
        [Required(ErrorMessage = "Полето 'Име' е задължително.")]
        [StringLength(SellerNameMaxLength, MinimumLength = SellerNameMinLength, ErrorMessage = "Името трябва да бъде между {2} и {1} символа дълго.")]
        [Display(Name = "Име*")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето 'Фамилия' е задължително.")]
        [StringLength(SellerNameMaxLength, MinimumLength = SellerNameMinLength, ErrorMessage = "Фамилията трябва да бъде между {2} и {1} символа дълга.")]
        [Display(Name = "Фамилия*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Полето 'Телефон' е задължително.")]
        [StringLength(SellerPhoneNumberMaxLength, MinimumLength = SellerPhoneNumberMinLength, ErrorMessage = "Телефонът трябва да бъде между {2} и {1} символа дълга.")]
        [Display(Name = "Телефон*")]
        public string PhoneNumber { get; set; }
    }
}
