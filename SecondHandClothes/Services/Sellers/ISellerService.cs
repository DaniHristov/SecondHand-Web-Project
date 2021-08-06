namespace SecondHandClothes.Services.Sellers
{
    public interface ISellerService
    {
        public bool IsSeller(string userId);

        public int SellerId(string userId);

        public int Create(
            string firstName,
            string lastName, 
            string phoneNumber, 
            string userId);
    }
}
