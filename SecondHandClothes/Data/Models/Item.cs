namespace SecondHandClothes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}
