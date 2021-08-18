namespace SecondHandClothes.Services.Cart
{
    using SecondHandClothes.Services.Cart.Models;
    using System.Collections.Generic;

    public interface ICartService
    {
        public IEnumerable<CartItemViewServiceModel> UsersCart(string userId);

        public bool AddProductToCart(string productId, string userId);

        public bool Delete(string productId, string user);
    }
}
