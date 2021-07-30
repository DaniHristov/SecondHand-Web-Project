namespace SecondHandClothes.Services.Products.Models
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public string Description { get; init; }

        public string SellerName { get; init; }

        public int SellerId { get; init; }

        public string UserId { get; init; }

        public string Category { get; init; }

        public string Colour { get; init; }

        public int CategoryId { get; init; }

        public string Size { get; init; }

        public int SizeId { get; init; }

        public int ConditionId { get; init; }

        public string Sex { get; init; }

        public int SexId { get; init; }

        public string CreatedOn { get; init; }
    }
}
