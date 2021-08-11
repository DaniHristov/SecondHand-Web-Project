namespace SecondHandClothes.Services.Products
{
    using System.Collections.Generic;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Models;
    using SecondHandClothes.Services.Products.Models;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string category,
            string manufacturer,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage);

        string Create(string title,
                string description,
                string colour,
                int conditionId,
                int categoryId,
                int sexId,
                string manufacturer,
                decimal price,
                int sizeId,
                string imageURL,
                int sellerId);

        bool Edit(
                string id,
                string title,
                string description,
                string colour,
                int conditionId,
                int categoryId,
                int sexId,
                string manufacturer,
                decimal price,
                int sizeId,
                string imageURL);

        public bool Delete(string productId);

        ProductDetailsServiceModel Details(string id);

        Product ProductById(string id);

        Product ProductByOrderId(string orderId);

        IEnumerable<ProductServiceModel> ProductsByUser(string userId);

        IEnumerable<string> AllBrands();

        IEnumerable<string> AllCategories();

        IEnumerable<ProductsCategoryServiceModel> GetCategories();

        IEnumerable<ProductsSexServiceModel> GetSexes();

        IEnumerable<ProductsConditionServiceModel> GetConditions();

        IEnumerable<ProductsSizeServiceModel> GetSizes();

        public bool CategoryExists(int categoryId);

        public bool SizeExists(int sizeId);

        public bool SexExists(int sexId);

        public bool ConditionExists(int conditionId);

        public bool IsBySeller(string productId, int sellerId);

        public bool IsProductFound(string productId);
    }
}
