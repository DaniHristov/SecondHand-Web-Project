namespace SecondHandClothes.Services.Sellers
{
    using System.Linq;
    using SecondHandClothes.Data;


    public class SellerService : ISellerService
    {
        private readonly SecondHandDbContext data;

        public SellerService(SecondHandDbContext data) 
            => this.data = data;

        public int GetSellerId(string userId)
            => this.data
                .Sellers
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .FirstOrDefault();


        public bool IsSeller(string userId)
            => this.data
                   .Sellers
                   .Any(s => s.UserId == userId);

    }
}
