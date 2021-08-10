namespace SecondHandClothes.Models.Orders
{
    using SecondHandClothes.Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class OrderViewFormModel
    {
        [Required(ErrorMessage = "Полето 'Име' е задължително.")]
        [StringLength(NameMaxLength,MinimumLength = NameMinLength,ErrorMessage = "Полето 'Име' трябва да бъде между {2} и {1} символа дълго.")]
        [Display(Name = "Име*")]
        public string FirstName { get; init; }

        [Required(ErrorMessage = "Полето 'Фамилия' е задължително.")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Полето 'Фамилия' трябва да бъде между {2} и {1} символа дълго.")]
        [Display(Name = "Фамилия*")]
        public string LastName { get; init; }

        [Required(ErrorMessage = "Полето 'Телефон' е задължително.")]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = "Полето 'Телефон' трябва да бъде между {2} и {1} символа дълго.")]
        [Display(Name = "Телефон*")]
        public string PhoneNumber { get; init; }

        [Required(ErrorMessage = "Полето 'Град' е задължително.")]
        [StringLength(OrderTownMaxLength,MinimumLength = OrderTownMinLength,ErrorMessage = "Полето 'Град' трябва да бъде между {2} и {1} символа дълго.")]
        [Display(Name = "Град")]
        public string Town { get; init; }

        [Required(ErrorMessage = "Полето 'Адрес за доставка' е задължително.")]
        [StringLength(OrderShippingAddressMaxLength, MinimumLength = OrderShippingAddressMinLength, ErrorMessage = "Полето 'Адрес за доставка' трябва да бъде между {2} и {1} символа дълго.")]
        [Display(Name = "Адрес за доставка*")]
        public string ShippingAddress { get; init; }

        [Display(Name = "Бележка")]
        [MaxLength(OrderNoteMaxLength , ErrorMessage = "Полето 'Бележка' може да бъде най-дълго до {1} символа.")]
        public string Note { get; init; }

        public OrderStatus Status { get; init; }

    }
}
