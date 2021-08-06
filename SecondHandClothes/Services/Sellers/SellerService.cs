namespace SecondHandClothes.Services.Sellers
{
    using System.Linq;
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;

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
    }
}
