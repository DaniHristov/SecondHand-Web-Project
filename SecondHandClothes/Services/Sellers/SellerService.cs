namespace SecondHandClothes.Services.Sellers
{
    using System.Linq;
    using SecondHandClothes.Data;


    public class SellerService : ISellerService
    {
        private readonly SecondHandDbContext data;

        public SellerService(SecondHandDbContext data) 
            => this.data = data;

        public bool IsSeller(string userId)
            => this.data
                   .Sellers
                   .Any(s => s.UserId == userId);
    }
}
