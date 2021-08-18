namespace SecondHandClothes.Tests.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using SecondHandClothes.Controllers;
    using Xunit;

    using static Data.Products;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
             => MyController<HomeController>
        .Instance(controller => controller
            .WithData(TenPublicProducts()))
        .Calling(c => c.Index())
        .ShouldReturn()
        .View();

        [Fact]
        public void ErrorShouldReturnView()
             => MyController<HomeController>
        .Instance()
        .Calling(c => c.Error())
        .ShouldReturn()
        .View();
    }
}
