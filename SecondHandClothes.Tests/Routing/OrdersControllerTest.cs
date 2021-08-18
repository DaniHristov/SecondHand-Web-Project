namespace SecondHandClothes.Tests.Routing
{
    using MyTested.AspNetCore.Mvc;
    using SecondHandClothes.Controllers;
    using SecondHandClothes.Models.Orders;
    using Xunit;

    public class OrdersControllerTest
    {
        [Fact]
        public void MakeOrderShoulBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Orders/MakeOrder")
            .To<OrdersController>(c => c.MakeOrder());

        [Theory]
        [InlineData("firstName", "lastName", "phoneNumber", "note", "address", "town")]
        public void PostMakeOrderShouldBeMapped(string firstName, string lastName, string phoneNumber, string note, string address, string town)
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithMethod(HttpMethod.Post)
            .WithLocation("/Orders/MakeOrder")
            .WithFormFields(new
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Note = note,
                ShippingAddress = address,
                Town = town
            }))
            .To<OrdersController>(c => c.MakeOrder(new OrderViewFormModel
            {
                FirstName = firstName,
                LastName = lastName,
                ShippingAddress = address,
                Note = note,
                PhoneNumber = phoneNumber,
                Town = town,
                Status = 0

            }))
            .AndAlso()
            .ToValidModelState();

        [Fact]
        public void MyIncomingOrdersShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Orders/MyIncomingOrders")
            .To<OrdersController>(c => c.MyIncomingOrders());


        [Fact]
        public void MyOutgoingOrdersShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Orders/MyOutgoingOrders")
            .To<OrdersController>(c => c.MyOutgoingOrders());

        [Fact]
        public void GetCompleteOrderShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Orders/CompleteOrder/1")
            .To<OrdersController>(c => c.CompleteOrder("1"));

        [Fact]
        public void PostCompleteOrderShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request=>request
            .WithMethod(HttpMethod.Post)
            .WithLocation("/Orders/CompleteOrder/1"))
            .To<OrdersController>(c => c.CompleteOrderConfirmed("1"));

        [Fact]
        public void GetDeclineOrderShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithMethod(HttpMethod.Post)
            .WithLocation("/Orders/DeclineOrder/1"))
            .To<OrdersController>(c => c.DeclineOrder("1"));

        [Fact]
        public void PostDeclineOrderShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithMethod(HttpMethod.Post)
            .WithLocation("/Orders/DeclineOrder/1"))
            .To<OrdersController>(c => c.DeclineOrderConfirmed("1"));


    }
}
