namespace SecondHandClothes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        public string CategoryName { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
