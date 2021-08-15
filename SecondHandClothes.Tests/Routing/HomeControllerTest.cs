namespace SecondHandClothes.Tests.Routing
{
    using MyTested.AspNetCore.Mvc;
    using SecondHandClothes.Controllers;
    using Xunit;
    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
    => MyRouting
        .Configuration()
        .ShouldMap("/")
        .To<HomeController>(c => c.Index(new Models.Home.ProductIndexViewModel
        {
        }));

        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
