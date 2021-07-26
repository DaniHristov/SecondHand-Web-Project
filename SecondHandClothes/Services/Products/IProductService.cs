namespace SecondHandClothes.Services.Products
{
    using System.Collections.Generic;
    using SecondHandClothes.Models;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string category,
            string manufacturer,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage);

        IEnumerable<string> AllProductBrands();

        IEnumerable<string> AllProductCategories();
    }
}
