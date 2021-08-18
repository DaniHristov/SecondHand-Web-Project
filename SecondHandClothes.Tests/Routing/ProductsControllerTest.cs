namespace SecondHandClothes.Tests.Routing

{
    using MyTested.AspNetCore.Mvc;
    using SecondHandClothes.Controllers;
    using SecondHandClothes.Models.Products;
    using Xunit;

    public class ProductsControllerTest
    {
        [Fact]
        public void MyProductsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/MyProducts")
                .To<ProductsController>(c => c.MyProducts());

        [Fact]
        public void AddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Add")
                .To<ProductsController>(c => c.Add());

        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Edit/test")
                .To<ProductsController>(c => c.Edit("test"));

        [Theory]
        [InlineData("testId", "TitleTest", "descr", "colour", 0, 0, 0, "mani", 0, 0, "image", "testId")]
        public void EditShouldBeMappedPost(string id, string title, string description, string colour, int conditionId, int categoryId, int sexId, string manufacturer, decimal price, int sizeId, string imageUrl, string userid)
    => MyRouting
        .Configuration()
        .ShouldMap(request => request
        .WithMethod(HttpMethod.Post)
        .WithLocation("/Products/Edit/test")
        .WithFormFields(new
        {
            Id = id,
            Title = title,
            Description = description,
            Colour = colour,
            ConditionId = conditionId,
            CategoryId = categoryId,
            SexId = sexId,
            Manufacturer = manufacturer,
            SizeId = sizeId,
            ImageUrl = imageUrl,
            Price = price,
            UserId = userid
        }))
        .To<ProductsController>(c => c.Edit("test", new ProductFormModel
        {
            Title = title,
            CategoryId = categoryId,
            Description = description,
            SexId = sexId,
            SizeId = sizeId,
            ImageURL = imageUrl,
            Colour = colour,
            ConditionId = conditionId,
            Manufacturer = manufacturer,
            Price = price
        }));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Products/Details/test")
            .To<ProductsController>(c => c.Details("test"));

        [Fact]
        public void GetDeleteShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Products/Delete/test")
            .To<ProductsController>(c => c.Delete("test"));

        [Fact]
        public void GetDeleteConfirmedShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
                .WithMethod(HttpMethod.Post)
                .WithLocation("/Products/Delete/test"))
            .To<ProductsController>(c => c.DeleteConfirmed("test"));
    }
}
