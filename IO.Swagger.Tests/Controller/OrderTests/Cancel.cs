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
    public class Cancel
    {
        [TestMethod]
        public void Should_Cancel_Order_Return_Success_ResponseCode()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Order order = new Order(){ OrderId = _guid.ToString() , Status = Status.Active};
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
           
            mockOrderService.Setup(o=>o.GetOrder(It.IsAny<string>()))
                            .Returns(order);
            mockOrderService.Setup(c => c.CancelOrder(It.IsAny<Order>()));
            
            OrderApiController controller = new OrderApiController(mockOrderService.Object);
            
            // Act
            var result = controller.CancelOrder(_guid.ToString());
            var checkresult = result as StatusCodeResult;

            // Assert
            Assert.AreEqual(200, checkresult.StatusCode);

        }
    }
}