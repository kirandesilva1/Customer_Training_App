using System;
using System.Collections.Generic;
using IO.Swagger.Controllers;
using IO.Swagger.Models;
using IO.Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IO.Swagger.UnitTests.Controller.OrderTests
{
    [TestClass]
    public class Get
    {
        [TestMethod]
        public void Should_Get_Orders_And_Return_Success_Status_Code()
        {
            // Arrange
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(o => o.GetOrders())
                .Returns(It.IsAny<IEnumerable<Order>>());
            OrderApiController controller = new OrderApiController(mockOrderService.Object);
            
            // Act
            var result = controller.GetOrders();
            var checkresult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(checkresult);
            Assert.AreEqual(200, checkresult.StatusCode);
            
        }
        
        [TestMethod]
        public void Should_Get_Customer_Orders_And_Return_Success_Status_Code()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();

            string customerId = _guid.ToString();
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(o => o.GetCustomerOrders(It.IsAny<string>()))
                .Returns(It.IsAny<IEnumerable<Order>>());
            OrderApiController controller = new OrderApiController(mockOrderService.Object);
            
            // Act
            var result = controller.GetCustomerOrders(customerId);
            var checkresult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(checkresult);
            Assert.AreEqual(200, checkresult.StatusCode);
            
        }
        
        
    }
}