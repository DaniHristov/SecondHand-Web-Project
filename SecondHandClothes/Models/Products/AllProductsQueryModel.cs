namespace SecondHandClothes.Models.Products
{
    using SecondHandClothes.Models.Products.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 2;

        [Display(Name = "Категория")]
        public string Category { get; init; }

        public IEnumerable<string> Categories { get; set; }

        [Display(Name = "Бранд")]   
        public string Manufacturer { get; init; }

        public IEnumerable<string> Manufacturers { get; set; }

        [Display(Name = "Търси по заглавие")]
        public string SearchTerm { get; init; }

        [Display(Name = "Сортиране")]
        public ProductSorting Sorting { get; init; }

        public int TotalProducts { get; set; }

        public int CurrentPage { get; init; } = 1;

        public IEnumerable<ListingProductsViewModel> Products { get; set; }
    }
}
