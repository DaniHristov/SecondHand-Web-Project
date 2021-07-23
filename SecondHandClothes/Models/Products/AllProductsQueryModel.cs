using SecondHandClothes.Models.Products.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandClothes.Models.Products
{
    public class AllProductsQueryModel
    {
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

        public IEnumerable<ListingProductsViewModel> Products { get; set; }
    }
}
