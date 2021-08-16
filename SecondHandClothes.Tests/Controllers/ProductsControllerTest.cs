namespace SecondHandClothes.Tests.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using SecondHandClothes.Controllers;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Models.Products;
    using SecondHandClothes.Services.Products;
    using System.Collections.Generic;
    using Xunit;

    using static Data.Products;

    public class ProductsControllerTest
    {

        [Fact]
        public void AllShouldReturnCorrectViewWithModel()
            => MyController<ProductsController>
            .Instance()
            .Calling(c => c.All(new AllProductsQueryModel
            {
            }))
            .ShouldReturn()
            .View(v => v.WithModelOfType<AllProductsQueryModel>());

        [Fact]
        public void MyProductsShouldHaveRestrictionForAuthorizedUsers()
                 => MyController<ProductsController>
                .Instance(c=>c.WithUser())
                .Calling(c => c.MyProducts())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void MyProductsShouldReturnCorrectViewWithModel()
            => MyController<ProductsController>
            .Instance(c => c.WithUser())
            .Calling(a => a.MyProducts())
            .ShouldHave()
              .ActionAttributes(attributes => attributes
                 .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(v => v.WithModelOfType<IEnumerable<ProductServiceModel>>());

        [Fact]
        public void AddGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
                => MyController<ProductsController>
                    .Instance(c => c.WithUser())
                    .Calling(c => c.Add())
                    .ShouldHave()
                    .ActionAttributes(attrs => attrs
                        .RestrictingForHttpMethod(HttpMethod.Get)
                        .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View(v=>v.WithModelOfType<ProductFormModel>());

        [Fact]
        public void GetAddActionShouldMapRouteAndReturnViewWithData() =>
                     MyMvc
                     .Pipeline()
                     .ShouldMap(request => request
                         .WithPath("/Products/Add")
                         .WithMethod(HttpMethod.Get)
                         .WithUser(TestUser.Identifier))
                     .To<ProductsController>(c => c.Add())
                     .Which()
                     .ShouldHave()
                     .ActionAttributes(attribute => attribute
                         .RestrictingForAuthorizedRequests())
                     .AndAlso()
                     .ShouldReturn()
                     .View(view => view
                         .WithModelOfType<ProductFormModel>());



    }
}
