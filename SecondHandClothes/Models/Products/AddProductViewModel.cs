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
        [StringLength(ProdcutTitleMaxLength,MinimumLength = ProductTitleMinLength)]
        [Display(Name = "Заглавие*")]
        public string Title { get; init; }

        [Required(ErrorMessage = "Полето 'Описание/Забележка' е задължително.")]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
        [Display(Name = "Забележки/Описание*")]
        public string Description { get; init; }

        [Required]
        [Display(Name = "Цена*")]
        public decimal Price { get; init; }

        [Display(Name = "Модел")]
        public string Model { get; init; }

        [Required]
        [Display(Name = "Цвят")]
        public string Colour { get; init; }

        [Display(Name = "URL-Снимка")]
        public string ImageUrl { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Телефон за връзка")]
        public string PhoneNumber { get; init; }

        [Display(Name = "Бранд")]
        public string Manufacturer { get; init; }

        [Display(Name = "Категория")]
        public int CategoryId { get; init; }    

        [Display(Name = "Състояние на продукта")]
        public int ConditionId { get; init; }

        [Display(Name = "Пол")]
        public int SexId { get; init; }

        [Display(Name = "Размер")]
        public int SizeId { get; set; }




        public ICollection<ProductImagesViewModel> ProductImages { get; set; }

        public IEnumerable<ProductsCategoryViewModel> Categories { get; set; }

        public IEnumerable<ProductsConditionViewModel> Conditions { get; set; }

        public IEnumerable<ProductsSexViewModel> Sexes { get; set; }

        public IEnumerable<ProductsSizeViewModel> Sizes { get; set; }



    }
}
//public string Id { get; init; } = Guid.NewGuid().ToString();

//[Required]
//[MaxLength(ProdcutTitleMaxLength)]
//public string Title { get; set; }

//[Required]
//[MaxLength(ProductDescriptionMaxLength)]
//public string Description { get; set; }

//[Required]
//public decimal Price { get; set; }

//public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

//public string Model { get; set; }

//[Required]
//public string Colour { get; set; }

//[Required]
//public string PhoneNumber { get; set; }

//public string Manufacturer { get; set; }

//public int ConditionId { get; set; }
//public Condition Condition { get; set; }

//public int SexId { get; set; }
//public Sex Sex { get; set; }

//public int CategoryId { get; set; }
//public Category Category { get; set; }

//public ICollection<ProductImage> ProductImages { get; init; }

//public ICollection<Comment> Comments { get; init; }