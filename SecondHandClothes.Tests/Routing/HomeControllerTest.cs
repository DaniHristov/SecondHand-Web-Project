namespace SecondHandClothes.Tests.Routing
{
    using MyTested.AspNetCore.Mvc;
    using SecondHandClothes.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        //[Theory]
        //[InlineData("test")]
        //public void IndexRouteShouldBeMapped(string id)
        //  => MyRouting
        //.Configuration()
        //.ShouldMap("/")
        //.To<HomeController>(c => c.Index(new Models.Home.ProductIndexViewModel
        //{
        //    Id = id,
        //    Title = "Test",
        //    Brand = "Test",
        //    Category = "test",
        //    ImageUrl = "test",
        //    Price = 25.32m
        //}));

        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
