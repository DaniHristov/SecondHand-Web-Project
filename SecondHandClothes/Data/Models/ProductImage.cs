namespace SecondHandClothes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductImage
    {
        public int Id { get; init; }

        [Required]
        [Url]
        public string ImageURL { get; set; }

        public Product Product { get; set; }
    }
}
