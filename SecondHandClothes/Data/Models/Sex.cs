using System.Collections.Generic;

namespace SecondHandClothes.Data.Models
{
    public class Sex
    {
        public int Id { get; set; }
        
        public string SexType { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
