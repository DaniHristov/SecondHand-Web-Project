namespace SecondHandClothes.Models.Home
{
    using System.Collections.Generic;
    using SecondHandClothes.Services.Home;

    public class IndexViewModel
    {
        public int TotalCustomers { get; set; }

        public int TotalSellers { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<IndexQueryServiceModel> Products { get; set; }
    }
}
