namespace SecondHandClothes.Models.Api.Products
{
    public class AllProductsApiRequestModel
    {
        public string Category { get; init; }

        public string Manufacturer { get; init; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public int TotalProducts { get; set; }

        public int ProductsPerPage { get; init; } = 10;

        public int CurrentPage { get; init; } = 1;
    }
}
