namespace SecondHandClothes.Tests.Services
{
    using System;
    using System.Linq;
    using Xunit;
    using CarRentingSystem.Test.Mocks;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Services.Products;

    using static ProductServiceTest;
    using SecondHandClothes.Services.Home;

    public class HomeServiceTest
    {
        [Fact]
        public void GetIndexProductsShouldNotBeNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var homeService = new HomeService(data);
            //Act
            var homeData = homeService.GetIndexProducts(product.Id, product.Manufacturer, product.Category.CategoryName, product.ImageURL, product.Title, product.Price);

            //Assert
            Assert.Equal(1, homeData.Count());
            Assert.NotNull(homeData);
        }
    }
}
