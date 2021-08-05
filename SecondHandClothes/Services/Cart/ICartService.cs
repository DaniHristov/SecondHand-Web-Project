namespace SecondHandClothes.Services.Cart
{
    using System.Collections.Generic;
    using SecondHandClothes.Services.Cart.Models;

    public interface ICartService
    {
        public IEnumerable<CartItemViewServiceModel> UsersCart(string userId);

        public bool AddProductToCart(string productId, string userId);

        public bool Delete(string productId, string user);
    }
}
