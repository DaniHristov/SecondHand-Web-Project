namespace SecondHandClothes.Services.Sellers
{
    using SecondHandClothes.Data.Models;

    public interface ISellerService
    {
        public bool IsSeller(string userId);

        public int SellerId(string userId);

        public Seller GetProductSellerById(int sellerId);

        public Seller GetSellerByUserId(string userId);

        public int Create(
            string firstName,
            string lastName,
            string phoneNumber,
            string userId);
    }
}
