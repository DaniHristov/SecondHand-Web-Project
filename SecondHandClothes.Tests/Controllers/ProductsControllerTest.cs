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


    }
}
