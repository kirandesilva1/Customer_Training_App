using System;
using IO.Swagger.Controllers;
using IO.Swagger.Models;
using IO.Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IO.Swagger.UnitTests.Controller.OrderTests
{
    [TestClass]
    public class Create
    {
        [TestMethod]
        public void Should_Create_Order_Return_Success_ResponseCode_And_Guid()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Order order = new Order(){ };
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(c => c.CreateOrder(It.IsAny<Order>()))
                .Returns(_guid);
            OrderApiController controller = new OrderApiController(mockOrderService.Object);
            
            // Act
            var result = controller.CreateOrder(order);
            var checkresult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(checkresult);
            Assert.AreEqual(_guid.ToString(),checkresult.Value.ToString());
            Assert.AreEqual(200, checkresult.StatusCode);

        }
        
    }
}