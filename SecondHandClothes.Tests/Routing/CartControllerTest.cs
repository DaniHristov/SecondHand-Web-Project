using MyTested.AspNetCore.Mvc;
using SecondHandClothes.Controllers;
using Xunit;

namespace SecondHandClothes.Tests.Routing
{
    public class CartControllerTest
    {
        [Fact]
        public void MyCartShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap($"/Cart/MyCart/?userId=1")
            .To<CartController>(c => c.MyCart("1"));

        [Fact]
        public void AddToCartPostShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request=>request
            .WithMethod(HttpMethod.Post)
            .WithLocation("/Cart/AddToCart/?productId=1&userId=1"))
            .To<CartController>(c => c.AddToCart("1", "1"));

        [Fact]
        public void DeletePostShouldBeMapped()
             => MyRouting
             .Configuration()
             .ShouldMap(request => request
             .WithMethod(HttpMethod.Post)
             .WithLocation("/Cart/Delete/?productId=1&userId=1"))
             .To<CartController>(c => c.Delete("1", "1"));

    }
}
