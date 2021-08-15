namespace SecondHandClothes.Tests.Controllers
{
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using SecondHandClothes.Controllers;
    using Xunit;
    using SecondHandClothes.Models.Sellers;
    using SecondHandClothes.Data.Models;

    public class SellersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
             => MyController<SellersController>
        .Instance()
        .Calling(c => c.Become())
        .ShouldHave()
        .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
        .AndAlso()
        .ShouldReturn()
        .View();

        [Theory]
        [InlineData("Dealer", "Delaerer", "0884723523")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
         string sellerFirstName,
         string sellerLastName,
         string phoneNumber)
            => MyController<SellersController>
             .Instance(controller => controller
                 .WithUser())
             .Calling(c => c.Become(new BecomeSellerFormModel
             {
                 FirstName = sellerFirstName,
                 LastName = sellerLastName,
                 PhoneNumber = phoneNumber
             }))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                 .RestrictingForHttpMethod(HttpMethod.Post)
                 .RestrictingForAuthorizedRequests())
             .ValidModelState()
             .Data(data => data
                 .WithSet<Seller>(dealers => dealers
                     .Any(d =>
                         d.FirstName == sellerFirstName &&
                         d.LastName == sellerLastName &&
                         d.PhoneNumber == phoneNumber &&
                         d.UserId == TestUser.Identifier)))
             .AndAlso()
             .ShouldReturn()
             .RedirectToAction("Add", "Products");

    }
}
