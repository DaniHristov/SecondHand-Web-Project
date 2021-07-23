using System.Collections.Generic;

namespace SecondHandClothes.Data.Models
{
    public class Size
    {
        public int Id { get; set; }

        public string SizeType { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}