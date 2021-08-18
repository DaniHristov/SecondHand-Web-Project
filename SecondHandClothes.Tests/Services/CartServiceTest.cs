namespace SecondHandClothes.Tests.Services
{
    using System;
    using System.Linq;
    using Xunit;
    using CarRentingSystem.Test.Mocks;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Services.Products;

    using static ProductServiceTest;
    using SecondHandClothes.Services.Cart.Models;

    public class CartServiceTest
    {
        [Fact]
        public void AddProductToCartShouldReturnTrue()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            var user = new User
            {
                Id = "UserId"
            };
            data.Users.Add(user);
            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var cartService = new CartService(data);


            //Act
            var cartData = cartService.AddProductToCart(product.Id, user.Id);

            //Assert
            Assert.True(cartData);
        }

        [Fact]
        public void AddProductToCartShouldReturnFalse()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var cartService = new CartService(data);


            //Act
            var cartData = cartService.AddProductToCart("fakeId", "fakeuserId");

            //Assert
            Assert.False(cartData);
        }


        [Fact]
        public void DeleteShouldReturnTrue()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            var user = new User
            {
                Id = "userIdTest"
            };
            data.Users.Add(user);
            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();
            

            var cartService = new CartService(data);
            cartService.AddProductToCart(product.Id, user.Id);

            //Act
            var cartData = cartService.Delete(product.Id, user.Id);

            //Assert
            Assert.True(cartData);
        }

        [Fact]
        public void DeleteShouldReturnFalse()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var user = new User
            {
                Id = "userIdTest"
            };
            data.Users.Add(user);
            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();


            var cartService = new CartService(data);
            cartService.AddProductToCart(product.Id, user.Id);

            //Act
            var cartData = cartService.Delete("fakeid", user.Id);

            //Assert
            Assert.False(cartData);
        }

        [Fact]
        public void UsersCartShouldReturnNotNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var user = new User
            {
                Id = "userIdTest"
            };
            data.Users.Add(user);
            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();


            var cartService = new CartService(data);
            cartService.AddProductToCart(product.Id, user.Id);

            //Act
            var cartData = cartService.UsersCart(user.Id);

            //Assert
            Assert.NotNull(cartData);
        }

        [Fact]
        public void UsersCartShouldBeEmpty()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var user = new User
            {
                Id = "userIdTest"
            };
            data.Users.Add(user);
            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var cartService = new CartService(data);
            cartService.AddProductToCart(product.Id, user.Id);

            //Act
            var cartData = cartService.UsersCart("fakeId");

            //Assert
            Assert.Empty(cartData);
        }
    }
}
