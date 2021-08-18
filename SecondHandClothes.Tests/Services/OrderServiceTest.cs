namespace SecondHandClothes.Tests.Services
{
    using System;
    using System.Linq;
    using Xunit;
    using CarRentingSystem.Test.Mocks;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Services.Products;

    using static ProductServiceTest;
    using SecondHandClothes.Services.Orders;
    using SecondHandClothes.Services.Cart.Models;
    using System.Collections.Generic;
    using SecondHandClothes.Services.Sellers;

    public class OrderServiceTest
    {
        [Fact]
        public void GetItemsShouldReturnNotNull()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var cartService = new CartService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();
            cartService.AddProductToCart(product.Id, order.UserId);
            
            //Act
            var orderData = orderService.GetItems(order.UserId);

            //Assert
            Assert.NotNull(orderData);

        }

        [Fact]
        public void GetItemsShouldBeNotEmpty()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var cartService = new CartService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();
            cartService.AddProductToCart(product.Id, order.UserId);

            //Act
            var orderData = orderService.GetItems(order.UserId);

            //Assert
            Assert.NotEmpty(orderData);
        }

        [Fact]
        public void GetIncomingOrdersShouldNotBeEmpty()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var cartService = new CartService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();
            cartService.AddProductToCart(product.Id, order.UserId);
            orderService.CreateOrder(order.FirstName, order.LastName, order.PhoneNumber, null, order.UserId, order.Note, order.SellerId, order.ProductId, order.ProductTitle, order.Price, order.Status, order.ShippingAddress, order.ImageURL, order.Town);

            //Act
            var orderData = orderService.GetIncomingOrders(order.SellerId);

            //Assert
            Assert.NotEmpty(orderData);
        }

        [Fact]
        public void GetIncomingOrdersShouldNotBeNull()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var cartService = new CartService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();
            cartService.AddProductToCart(product.Id, order.UserId);
            orderService.CreateOrder(order.FirstName, order.LastName, order.PhoneNumber, null, order.UserId, order.Note, order.SellerId, order.ProductId, order.ProductTitle, order.Price, order.Status, order.ShippingAddress, order.ImageURL, order.Town);

            //Act
            var orderData = orderService.GetIncomingOrders(order.SellerId);

            //Assert
            Assert.NotNull(orderData);
        }

        [Fact]
        public void GetIncomingOrdersShouldBeEmpty()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var cartService = new CartService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.SaveChanges();
            
            //Act
            var orderData = orderService.GetIncomingOrders(order.SellerId);

            //Assert
            Assert.Empty(orderData);
        }

        [Fact]
        public void GetOutgoingOrdersShouldBeEmpty()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var sellerService = new SellerService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.SaveChanges();
            var seller = sellerService.GetProductSellerById(order.SellerId);

            //Act
            var orderData = orderService.GetOutgoingOrders(seller, order.UserId);

            //Assert
            Assert.Empty(orderData);
        }

        [Fact]
        public void GetOutgoingOrdersShouldNotBeEmpty()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var sellerService = new SellerService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.SaveChanges();
            var seller = sellerService.GetProductSellerById(order.SellerId);
            orderService.CreateOrder(order.FirstName, order.LastName, order.PhoneNumber, null, order.UserId, order.Note, order.SellerId, order.ProductId, order.ProductTitle, order.Price, order.Status, order.ShippingAddress, order.ImageURL, order.Town);

            //Act
            var orderData = orderService.GetOutgoingOrders(seller, order.UserId);

            //Assert
            Assert.NotEmpty(orderData);
        }

        [Fact]
        public void GetOutgoingOrdersShouldNotBeNull()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var sellerService = new SellerService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.SaveChanges();
            var seller = sellerService.GetProductSellerById(order.SellerId);
            orderService.CreateOrder(order.FirstName, order.LastName, order.PhoneNumber, null, order.UserId, order.Note, order.SellerId, order.ProductId, order.ProductTitle, order.Price, order.Status, order.ShippingAddress, order.ImageURL, order.Town);

            //Act
            var orderData = orderService.GetOutgoingOrders(seller, order.UserId);

            //Assert
            Assert.NotNull(orderData);
        }

        [Fact]
        public void GetOrderByIdShouldReturnNotNull()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var sellerService = new SellerService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();
            orderService.CreateOrder(order.FirstName, order.LastName, order.PhoneNumber, null, order.UserId, order.Note, order.SellerId, order.ProductId, order.ProductTitle, order.Price, order.Status, order.ShippingAddress, order.ImageURL, order.Town);

            //Act
            var orderData = orderService.GetOrderById(order.Id);

            //Assert
            Assert.NotNull(orderData);
        }

        [Fact]
        public void GetOrderByIdShouldReturnNull()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var sellerService = new SellerService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.SaveChanges();
            orderService.CreateOrder(order.FirstName, order.LastName, order.PhoneNumber, null, order.UserId, order.Note, order.SellerId, order.ProductId, order.ProductTitle, order.Price, order.Status, order.ShippingAddress, order.ImageURL, order.Town);

            //Act
            var orderData = orderService.GetOrderById(order.Id);

            //Assert
            Assert.Null(orderData);
        }

        [Fact]
        public void GetOrderByIdShouldReturnSameOrder()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var sellerService = new SellerService(data);
            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();
            orderService.CreateOrder(order.FirstName, order.LastName, order.PhoneNumber, null, order.UserId, order.Note, order.SellerId, order.ProductId, order.ProductTitle, order.Price, order.Status, order.ShippingAddress, order.ImageURL, order.Town);

            //Act
            var orderData = orderService.GetOrderById(order.Id);

            //Assert
            Assert.Equal(order,orderData);
        }


        [Fact]
        public void CreateShouldReturnNotNull()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            var orderData = orderService.CreateOrder(order.FirstName, order.LastName, order.PhoneNumber, null, order.UserId, order.Note, order.SellerId, order.ProductId, order.ProductTitle, order.Price, order.Status, order.ShippingAddress, order.ImageURL, order.Town);

            //Assert
            Assert.NotNull(orderData);
        }

        [Fact]
        public void CompleteOrderShouldWorkProperly()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            orderService.CompleteOrder(product, order);

            //Assert
            Assert.Equal(0,data.Products.Count());
        }

        [Fact]
        public void CompleteOrderShouldSetCorrectStatus()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            orderService.CompleteOrder(product, order);

            //Assert
            Assert.Equal(OrderStatus.Изпълнена, order.Status);
        }


        [Fact]
        public void DeclineOrderShouldWorkProperly()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            orderService.DeclineOrder(order);

            //Assert
            Assert.Equal(1, data.Products.Count());
        }

        [Fact]
        public void DeclineOrderShouldSetCorrectStatus()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            orderService.DeclineOrder(order);

            //Assert
            Assert.Equal(OrderStatus.Отказана, order.Status);
        }

        [Fact]
        public void GetProductDataShouldReturnNotNull()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = new Product
            {
                CategoryId = 1,
                Description = "description",
                SellerId = 1,
                SexId = 1,
                SizeId = 1,
                Colour = "Colour",
                CreatedOn = DateTime.UtcNow,
                ConditionId = 1,
                Id = "productId",
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
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            var orderData = orderService.GetProductData(order.ProductId);

            //Assert
            Assert.NotNull(orderData);
        }

        [Fact]
        public void GetProductDataShouldReturnNull()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            var orderData = orderService.GetProductData(order.ProductId);

            //Assert
            Assert.Null(orderData);
        }

        [Fact]
        public void GetProductDataShouldReturnSameProduct()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = new Product
            {
                CategoryId = 1,
                Description = "description",
                SellerId = 1,
                SexId = 1,
                SizeId = 1,
                Colour = "Colour",
                CreatedOn = DateTime.UtcNow,
                ConditionId = 1,
                Id = "productId",
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
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            var orderData = orderService.GetProductData(order.ProductId);

            //Assert
            Assert.Equal(product,orderData);
        }

        [Fact]
        public void IsAlreadyOrderedShouldReturnTrue()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            var orderData = orderService.IsAlreadyOrdered(order.UserId, order.ProductId);

            //Assert
            Assert.True(orderData);
        }

        [Fact]
        public void IsAlreadyOrderedShouldReturnFalse()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.SaveChanges();


            //Act
            var orderData = orderService.IsAlreadyOrdered(order.UserId, order.ProductId);

            //Assert
            Assert.False(orderData);
        }


        [Fact]
        public void GetOrderBySellerIdShouldReturnNotNull()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            var orderData = orderService.GetOrdersBySeller(order.SellerId);

            //Assert
            Assert.NotNull(orderData);
        }


        [Fact]
        public void GetOrderBySellerIdShouldReturnNotEmpty()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.Orders.Add(order);
            data.SaveChanges();


            //Act
            var orderData = orderService.GetOrdersBySeller(order.SellerId);

            //Assert
            Assert.NotEmpty(orderData);
        }

        [Fact]
        public void GetOrderBySellerIdShouldReturnEmpty()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var product = CreateProduct();
            var order = CreateOrder();

            var orderService = new OrderService(data);
            data.Products.Add(product);
            data.SaveChanges();


            //Act
            var orderData = orderService.GetOrdersBySeller(order.SellerId);

            //Assert
            Assert.Empty(orderData);
        }

        public static Order CreateOrder()
        {
            return new Order
            {
                Id = "testId",
                CreatedOn = DateTime.UtcNow,
                FirstName = "firstName",
                LastName = "lastName",
                SellerId = 1,
                Sellers = null,
                ShippingAddress = "address",
                Status = OrderStatus.Изчакване,
                ImageURL = "https://www.publicdomainpictures.net/pictures/320000/velka/background-image.png",
                Note = "note",
                PhoneNumber = "phoneNumber",
                Price = 25.24m,
                ProductId = "productId",
                Products = null,
                ProductTitle = "title",
                Town = "town",
                User = new User { Id = "user" },
                UserId = "user"
            };
        }
    }
}
