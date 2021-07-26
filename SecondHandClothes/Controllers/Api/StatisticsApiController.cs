namespace SecondHandClothes.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using SecondHandClothes.Services;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
            => this.statistics = statistics;

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
            => this.statistics.Total();
    }
}
