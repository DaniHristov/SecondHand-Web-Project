namespace SecondHandClothes.Tests.Services
{
    using CarRentingSystem.Test.Mocks;
    using SecondHandClothes.Services.Sellers;
    using System.Linq;
    using Xunit;
    using static ProductServiceTest;

    public class SellerServiceTest
    {
        [Fact]
        public void CreateSellerShouldAddSellerToDatabaseSuccessfully()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var sellerService = new SellerService(data);
            var seller = CreateSeller();


            //Act
            var sellerData = sellerService.Create(seller.FirstName, seller.LastName, seller.PhoneNumber, seller.UserId);


            //Assert
            Assert.Equal(1, data.Sellers.Count());
        }

        [Fact]
        public void GetProductSellerByIdShouldReturnNotNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var sellerService = new SellerService(data);
            var seller = CreateSeller();
            data.Sellers.Add(seller);
            data.SaveChanges();

            //Act
            var sellerData = sellerService.GetProductSellerById(seller.Id);

            //Assert
            Assert.NotNull(sellerData);
        }

        [Fact]
        public void GetProductSellerByIdShouldReturnNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var sellerService = new SellerService(data);
            var seller = CreateSeller();
            data.Sellers.Add(seller);
            data.SaveChanges();

            //Act
            var sellerData = sellerService.GetProductSellerById(132);

            //Assert
            Assert.Null(sellerData);
        }

        [Fact]
        public void GetProductSellerByUserIdShouldReturnNotNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var sellerService = new SellerService(data);
            var seller = CreateSeller();
            data.Sellers.Add(seller);
            data.SaveChanges();

            //Act
            var sellerData = sellerService.GetSellerByUserId(seller.UserId);

            //Assert
            Assert.NotNull(sellerData);
        }

        [Fact]
        public void GetProductSellerByUserIdShouldReturnNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var sellerService = new SellerService(data);
            var seller = CreateSeller();
            data.Sellers.Add(seller);
            data.SaveChanges();

            //Act
            var sellerData = sellerService.GetSellerByUserId("userid");

            //Assert
            Assert.Null(sellerData);
        }

        [Fact]
        public void IsSellerShouldReturnTrue()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var sellerService = new SellerService(data);
            var seller = CreateSeller();
            data.Sellers.Add(seller);
            data.SaveChanges();

            //Act
            var sellerData = sellerService.IsSeller(seller.UserId);

            //Assert
            Assert.True(sellerData);
        }

        [Fact]
        public void IsSellerShouldReturnFalse()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var sellerService = new SellerService(data);
            var seller = CreateSeller();
            data.Sellers.Add(seller);
            data.SaveChanges();

            //Act
            var sellerData = sellerService.IsSeller("falseId");

            //Assert
            Assert.False(sellerData);
        }

        [Fact]
        public void SellerIdShouldReturnRightSellerId()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var sellerService = new SellerService(data);
            var seller = CreateSeller();
            data.Sellers.Add(seller);
            data.SaveChanges();

            //Act
            var sellerData = sellerService.SellerId(seller.UserId);

            //Assert
            Assert.Equal(sellerData, seller.Id);
        }

        [Fact]
        public void SellerIdShouldReturnNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var sellerService = new SellerService(data);
            var seller = CreateSeller();
            data.Sellers.Add(seller);
            data.SaveChanges();

            //Act
            var sellerData = sellerService.SellerId("null");

            //Assert
            Assert.Equal(0, sellerData);
        }

    }
}
