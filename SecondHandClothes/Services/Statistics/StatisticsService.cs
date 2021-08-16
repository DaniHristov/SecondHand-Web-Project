namespace SecondHandClothes.Services.Statistics
{
    using System.Linq;
    using SecondHandClothes.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly SecondHandDbContext data;

        public StatisticsService(SecondHandDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalProducts = this.data.Products.Count(p=>p.IsPublic);
            var totalUsers = this.data.Users.Count();
            var totalSellers = this.data.Sellers.Count();

            return new StatisticsServiceModel
            {
                TotalProducts = totalProducts,
                TotalCustomers = totalUsers,
                TotalSellers =  totalSellers
            };
        }
    }
}
