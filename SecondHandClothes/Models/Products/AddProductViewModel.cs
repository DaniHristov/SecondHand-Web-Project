namespace SecondHandClothes.Models.Products
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Models.ProductImages;

    using static Data.DataConstants;
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Полето 'Заглавие' е задължително.")]
        [StringLength(ProdcutTitleMaxLength,MinimumLength = ProductTitleMinLength,
            ErrorMessage = "Заглавието трябва да бъде между {2} и {1} символа дълго!")]
        [Display(Name = "Заглавие*")]
        public string Title { get; init; }

        [Required(ErrorMessage = "Полето 'Описание/Забележка' е задължително.")]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
        [Display(Name = "Забележки/Описание*")]
        public string Description { get; init; }

        [Required(ErrorMessage = "Полето 'Цена' е задължително.")]
        [Range(ProductMinPrice,10000 ,ErrorMessage = "Цената трябва да е поне {1}лв")]
        [Display(Name = "Цена*")]
        public decimal Price { get; init; }

        [Required]
        [Display(Name = "Цвят*")]
        public string Colour { get; init; }

        [Display(Name = "URL-Снимка*")]
        [Url]
        [Required(ErrorMessage = "Полето 'URL-Снимка' е задължително.")]
        public string ImageURL { get; set; }

        [Display(Name = "Бранд*")]
        [Required(ErrorMessage = "Полето 'Бранд' е задължително.")]
        public string Manufacturer { get; init; }

        [Display(Name = "Категория*")]
        [Required]
        public int CategoryId { get; init; }    

        [Display(Name = "Състояние на продукта*")]
        public int ConditionId { get; init; }

        [Display(Name = "Пол")]
        public int SexId { get; init; }

        [Display(Name = "Размер*")] 
        public int SizeId { get; set; }

        public ICollection<ProductImagesViewModel> ProductImages { get; set; }

        public IEnumerable<ProductsCategoryViewModel> Categories { get; set; }

        public IEnumerable<ProductsConditionViewModel> Conditions { get; set; }

        public IEnumerable<ProductsSexViewModel> Sexes { get; set; }

        public IEnumerable<ProductsSizeViewModel> Sizes { get; set; }



    }
}
//TODO : Опиши грешките