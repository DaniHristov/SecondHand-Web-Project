namespace SecondHandClothes.Services.Sellers
{
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using System.Linq;

    public class SellerService : ISellerService
    {
        private readonly SecondHandDbContext data;

        public SellerService(SecondHandDbContext data)
            => this.data = data;

        public int SellerId(string userId)
            => this.data
                .Sellers
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .FirstOrDefault();


        public bool IsSeller(string userId)
            => this.data
                   .Sellers
                   .Any(s => s.UserId == userId);

        public int Create(string firstName, string lastName, string phoneNumber, string userId)
        {

            var sellerData = new Seller
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                UserId = userId
            };

            this.data.Sellers.Add(sellerData);
            this.data.SaveChanges();

            return sellerData.Id;
        }

        public Seller GetProductSellerById(int sellerId)
            => this.data.Sellers.Find(sellerId);

        public Seller GetSellerByUserId(string userId)
            => this.data
                    .Sellers
                    .FirstOrDefault(s => s.UserId == userId);
    }
}
