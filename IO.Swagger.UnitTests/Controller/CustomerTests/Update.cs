using System;
using IO.Swagger.Controllers;
using IO.Swagger.Models;
using IO.Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IO.Swagger.UnitTests.Controller
{
    [TestClass]
    public class Update
    {
        [TestMethod]
        public void Should_Update_Customer_Thats_Not_Null_And_Return_Success_StatusCode()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Customer originalCustomer = new Customer(){ Id = _guid, Name = "Moe" , Groupid = 1};
            Customer updatedCustomer = new Customer(){ Id = _guid, Name = "Moe Sweeney" , Groupid = 2};
            
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            
            mockCustomerService.Setup(c => c.GetCustomerInfo(It.IsAny<string>()))
                .Returns(originalCustomer);
            
            mockCustomerService.Setup(x => x.UpdateCustomer(It.IsAny<Customer>()));
                 
            CustomerApiController controller = new CustomerApiController(mockCustomerService.Object,mockOrderService.Object);
            
            // Act
            var result = controller.UpdateCustomer(updatedCustomer);
            var checkResult = result as StatusCodeResult;
            
            // Assert
            Assert.IsNotNull(checkResult);
            Assert.AreEqual(200, checkResult.StatusCode);
        }
        
    }
}