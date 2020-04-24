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
    public class GetOrder
    {
        [TestMethod]
        public void Should_Get_Order_ById_Success_Status_Code()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Order order = new Order(){ Id = _guid, Description = "Order Description"};
            
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(o => o.GetOrder(It.IsAny<String>()))
                .Returns(order);
            OrderApiController controller = new OrderApiController(mockOrderService.Object);
            
            // Act
            var result = controller.GetOrder(_guid.ToString());
            var checkresult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(checkresult);
            Assert.AreEqual(200, checkresult.StatusCode);
            
        }
        
        
    }
}