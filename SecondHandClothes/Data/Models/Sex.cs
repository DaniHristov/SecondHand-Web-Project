namespace SecondHandClothes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Sex
    {
        public int Id { get; set; }
        
        [Required]
        public string SexType { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
