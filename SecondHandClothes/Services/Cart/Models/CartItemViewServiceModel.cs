using SecondHandClothes.Data.Models;

namespace SecondHandClothes.Services.Cart.Models
{
    public class CartItemViewServiceModel
    {
        public string ProductId { get; init; }

        public string Title { get; init; }

        public string ImageURL { get; init; }

        public string Brand { get; init; }

        public decimal Price { get; init; }

        public int SellerId { get; init; }

        public Seller Seller { get; init; }
    }
}
