namespace SecondHandClothes.Services.Cart.Models
{
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CartService : ICartService
    {
        private readonly SecondHandDbContext data;

        public CartService(SecondHandDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<CartItemViewServiceModel> UsersCart(string userId)
        {
            var sellerId = this.data.Products.Select(p => p.SellerId).FirstOrDefault();
            var seller = this.data.Sellers.FirstOrDefault(s => s.Id == sellerId);
            
            return data.CartItems
            .Where(sci => sci.UserId == userId)
            .Select(sci => new CartItemViewServiceModel
            {
                Brand = sci.Product.Manufacturer,
                ImageURL = sci.Product.ImageURL,
                Price = sci.Product.Price,
                Seller = seller,
                SellerId = sellerId,
                ProductId = sci.ProductId,
                Title = sci.Product.Title
            })
            .ToList();
        }

        public bool AddProductToCart(string productId, string userId)
        {
            var user = data.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var product = data.Products
                .Where(p => p.Id == productId)
                .FirstOrDefault();

            if (product == null)
            {
                return false;
            }

            if (this.data.CartItems.Where(x=>x.UserId==userId).Any(x=>x.ProductId==productId))
            {
                return false;
            }

            var cartItem = new Item()
            {
                UserId = userId,
                ProductId = productId
            };

            data.CartItems.Add(cartItem);
            data.SaveChanges();
            
            return true;
        }

        public bool Delete(string productId, string userId)
        {
            var product = this.data.CartItems.Where(p => p.ProductId == productId && p.UserId == userId).FirstOrDefault();
            if (product == null)
            {
                return false;
            }

            this.data.CartItems.Remove(product);
            this.data.SaveChanges();
            return true;
        }
    }
}
