namespace SecondHandClothes.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using SecondHandClothes.Services.Products;

    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 6;

        [Display(Name = "Категория")]
        public string Category { get; init; }


        [Display(Name = "Бранд")]   
        public string Manufacturer { get; init; }


        [Display(Name = "Търси по заглавие")]
        public string SearchTerm { get; init; }

        [Display(Name = "Сортиране")]
        public ProductSorting Sorting { get; init; }

        public int TotalProducts { get; set; }

        public int CurrentPage { get; init; } = 1;

        public IEnumerable<string> Manufacturers { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}
