namespace SecondHandClothes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Size
    {
        public int Id { get; set; }

        [Required]
        public string SizeType { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}