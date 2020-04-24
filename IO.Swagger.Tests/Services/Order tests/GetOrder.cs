using System;
using System.Threading.Tasks;
using IO.Swagger.Controllers;
using IO.Swagger.Data;
using IO.Swagger.Models;
using IO.Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IO.Swagger.UnitTests.Services.Order_tests
{
    [TestClass]
    public class GetOrder
    {
        [TestMethod]
        public void Should_Get_Order_By_Id()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Order order = new Order(){ Id = _guid, OrderId = "testid-" + _guid, Description = "Order Description"};
            
            Mock<IRepository<Order>> repository = new Mock<IRepository<Order>>();
            repository
                .Setup(x => x.GetById(_guid))
                .Returns(order);
            
           IOrderService orderService = new OrderService(repository.Object);

           // ACT
           var result = orderService.GetOrder(_guid.ToString());

           // ASSERT
           Assert.IsNotNull(result);
           Assert.AreEqual("testid-" + _guid, result.OrderId);
        }

        [TestMethod]
        public void Should_Return_Null_For_Invalid_Order_Id()
        {
            // ARRANGE
            Mock<IRepository<Order>> repository = new Mock<IRepository<Order>>();
            IOrderService orderService = new OrderService(repository.Object);

            // ACT
            var result = orderService.GetOrder("");
            
            // ASSERT
            Assert.IsNull(result);
        }
        
    }
}