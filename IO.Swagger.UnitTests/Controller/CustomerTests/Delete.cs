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
    public class Delete
    {
        [TestMethod]
        public void Should_Delete_Customer_Thats_Not_Null_And_Return_Success_StatusCode()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Customer originalCustomer = new Customer(){ Id = _guid, Name = "Moe" , Groupid = 1};
            
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            mockCustomerService.Setup(c => c.GetCustomerInfo(It.IsAny<string>()))
                .Returns(originalCustomer);
            
            mockCustomerService.Setup(x => x.DeleteCustomer(It.IsAny<Customer>()));
                 
            CustomerApiController controller = new CustomerApiController(mockCustomerService.Object, mockOrderService.Object);
            
            // Act
            var result = controller.DeleteCustomer(_guid.ToString());
            var checkResult = result as StatusCodeResult;
            
            // Assert
            Assert.IsNotNull(checkResult);
            Assert.AreEqual(200, checkResult.StatusCode);
        }
        
        [TestMethod]
        public void Should_NOT_Delete_NonExistent_Customer_Return_Correct_StatusCode()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Customer nonExistentCustomer = null;
            
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            mockCustomerService.Setup(c => c.GetCustomerInfo(It.IsAny<string>()))
                .Returns(nonExistentCustomer);
            
            CustomerApiController controller = new CustomerApiController(mockCustomerService.Object, mockOrderService.Object);
            
            // Act
            var result = controller.DeleteCustomer(_guid.ToString());
            var checkResult = result as StatusCodeResult;
            
            // Assert
            Assert.IsNotNull(checkResult);
            Assert.AreEqual(404, checkResult.StatusCode);
        }
        
        
        
    }
}