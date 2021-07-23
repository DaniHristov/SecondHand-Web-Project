namespace SecondHandClothes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Product
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(ProdcutTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string Colour { get; set; }

        public string Manufacturer { get; set; }

        [Required]
        [Url]
        public string ImageURL { get; set; }

        [Required]
        [MaxLength(ProductSizeMaxLength)]
        public int SizeId { get; set; }
        public Size Size { get; set; }

        public int ConditionId { get; set; }
        public Condition Condition { get; set; }

        public int SexId { get; set; }
        public Sex Sex { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //public ICollection<ProductImage> ProductImages { get; init; }

        public ICollection<Comment> Comments { get; init; }

    }
}
