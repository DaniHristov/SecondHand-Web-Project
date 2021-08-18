using MyTested.AspNetCore.Mvc;
using SecondHandClothes.Controllers;
using SecondHandClothes.Models.Sellers;
using Xunit;

namespace SecondHandClothes.Tests.Routing
{
    public class SellersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Sellers/Become")
            .To<SellersController>(c => c.Become());

        [Theory]
        [InlineData("firstName","lastName","0884736604")]
        public void PostBecomeShouldBeMapped(string firstName,string lastName , string phoneNumber )
             => MyRouting
             .Configuration()
             .ShouldMap(request=>request
             .WithMethod(HttpMethod.Post)
             .WithLocation("/Sellers/Become")
             .WithFormFields(new
             {
                 FirstName = firstName,
                 LastName = lastName,
                 phoneNumber = phoneNumber
             }))
             .To<SellersController>(c => c.Become(new BecomeSellerFormModel
             {
                 FirstName = firstName,
                 LastName = lastName,
                 PhoneNumber = phoneNumber
             }))
            .AndAlso()
            .ToValidModelState();

    }
}
