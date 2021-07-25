namespace SecondHandClothes.Models.Home
{
    using SecondHandClothes.Data.Models;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalCustomers { get; set; }

        public int TotalSellers { get; set; }

        public int TotalProducts { get; set; }

        public List<ProductIndexViewModel> Products { get; set; }
    }
}
