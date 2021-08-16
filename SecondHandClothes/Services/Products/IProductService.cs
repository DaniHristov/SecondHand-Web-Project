namespace SecondHandClothes.Services.Products
{
    using System.Collections.Generic;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Models;
    using SecondHandClothes.Services.Products.Models;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string category = null,
            string manufacturer = null,
            string searchTerm = null,
            ProductSorting sorting = ProductSorting.CreatedOn,
            int currentPage = 1,
            int productsPerPage = int.MaxValue,
            bool publicOnly = true);

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
                string imageURL,
                bool isPublic);

        public void ChangeVisibility(string productId);

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
