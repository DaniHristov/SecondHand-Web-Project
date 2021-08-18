namespace SecondHandClothes.Tests.Services
{
    using System;
    using System.Linq;
    using Xunit;
    using CarRentingSystem.Test.Mocks;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Services.Products;

    public class ProductServiceTest
    {
        [Fact]
        public void ProductCreateShouldAddProductToDatabase()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();

            var productService = new ProductService(data);


            //Act
            var productData = productService.Create(product.Title, product.Description, product.Colour, product.ConditionId, product.CategoryId, product.SexId, product.Manufacturer, product.Price, product.SizeId, product.ImageURL, product.SellerId);

            //Assert
            Assert.Equal(1, data.Products.Count());

        }

        [Fact]
        public void ProductDetailsShouldReturnDetails()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            var productId = product.Id;

            //Act
            var productData = data.Products.Find(productId);

            //Assert
            Assert.NotNull(productData);

        }

        [Fact]
        public void CategoryNotExistingShouldReturnTrue()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            var product = CreateProduct();



            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.CategoryExists(2);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void SizeNotExistingShouldReturnTrue()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.SizeExists(2);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void SexNotExistingShouldReturnTrue()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            var product = CreateProduct();


            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.SexExists(2);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void ConditionNotExistingShouldReturnTrue()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.ConditionExists(2);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void ProductIsBySellerShouldReturnTrue()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.IsBySeller(product.Id, product.SellerId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ProductDeleteShouldReturnTrue()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.Delete(product.Id);

            //Assert
            Assert.True(result);
            Assert.Equal(0, data.Products.Count());
        }

        [Fact]
        public void ProductEditShouldReturnTrue()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.Edit(product.Id, product.Title, product.Description, product.Colour, product.ConditionId, product.CategoryId, product.SexId, product.Manufacturer, product.Price, product.SizeId, product.ImageURL
                , product.IsPublic);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ProductAllBrandsShouldReturnCorrectlyAllBrands()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.AllBrands();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProductGetAllSexesShouldReturnCorrectlyAllSexes()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.GetSexes();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProductGetAllSizesShouldReturnCorrectlyAllSizes()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.GetSizes();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProductGetAllConditionsShouldReturnCorrectlyAllConditions()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.GetConditions();

            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void ProductAllCategoriesShouldReturnCorrectlyAllCategories()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();

            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.AllCategories();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProductDetailsShouldReturnProductWithDetails()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.Details(product.Id);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProductsAllShouldReturnAllProducts()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.All();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IsProductFoundShouldReturnTrue()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.IsProductFound(product.Id);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void IsProductFoundShouldReturnFalse()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.IsProductFound("fakeid");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ProductByIdShouldReturnNotNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.ProductById(product.Id);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProductByOrderIdShouldReturnNotNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.ProductByOrderId(product.Id);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProductByUserShouldReturnNotNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.ProductsByUser(product.Seller.UserId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProductByUserShouldReturnNull()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            data.Products.Add(product);
            data.SaveChanges();

            var productService = new ProductService(data);

            //Act
            var result = productService.ProductsByUser("fakeID");

            //Assert
            Assert.Empty(result);
        }



        public static Seller CreateSeller()
        {
            return new Seller
            {
                FirstName = "Test",
                LastName = "Test",
                Id = 1,
                Orders = null,
                PhoneNumber = "087412423",
                Products = null,
                UserId = "userIdTest"
            };
        }

        public static Product CreateProduct()
        {
            return new Product
            {
                CategoryId = 1,
                Description = "description",
                SellerId = 1,
                SexId = 1,
                SizeId = 1,
                Colour = "Colour",
                CreatedOn = DateTime.UtcNow,
                ConditionId = 1,
                Id = "1",
                ImageURL = "https://media.wired.com/photos/5b899992404e112d2df1e94e/master/pass/trash2-01.jpg",
                IsPublic = true,
                Manufacturer = "Brand",
                Title = "title",
                Price = 25.23m,
                Seller = CreateSeller(),
                Size = new Size { Id = 1, SizeType = "testSize" },
                Sex = new Sex { Id = 1, SexType = "testSex" },
                Category = new Category { Id = 1, CategoryName = "testCategory" },
                Condition = new Condition { Id = 1, ConditionType = "testCondition" }
            };
        }
    }
}
