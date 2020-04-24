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
    public class Update
    {
        [TestMethod]
        public void Should_Update_Order_Thats_Not_Null_And_Return_Success_StatusCode()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Order originalOrder = new Order(){ Id = _guid, Description = "Test Order", 
                Shipaddress = new Address(){ Streetname = "Street 1" , Zipcode = "15214"}, 
                OrderItems = new List<OrderItem>() { new OrderItem(){ ItemName = "Item 1" , Qty = 12, Cost = 1 , UnitPrice = 12 }} };
            
            Order updatedOrder = new Order(){ Id = _guid, Description = "Test Order", 
                Shipaddress = new Address(){ Streetname = "Street 1" , Zipcode = "15214"}, 
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem(){ ItemName = "Item 1" , Qty = 12, Cost = 1 , UnitPrice = 12 }, 
                    new OrderItem(){ ItemName = "Item 2" , Qty = 14, Cost = 1 , UnitPrice = 14 }
                } 
            };
            
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            
            mockOrderService.Setup(x => x.GetOrder(It.IsAny<string>()))
                .Returns(originalOrder);
            
            mockOrderService.Setup(x => x.UpdateOrder(It.IsAny<Order>()));
                 
            OrderApiController controller = new OrderApiController(mockOrderService.Object);
            
            // Act
            var result = controller.UpdateOrder(updatedOrder);
            var checkResult = result as StatusCodeResult;
            
            // Assert
            Assert.IsNotNull(checkResult);
            Assert.AreEqual(200, checkResult.StatusCode);
        }
    }
}