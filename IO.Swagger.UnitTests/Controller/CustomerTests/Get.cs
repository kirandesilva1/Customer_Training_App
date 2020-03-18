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
    public class Get
    {
        [TestMethod]
        public void Should_Get_Customer_And_Return_Success_Status_Code()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Customer customer = new Customer(){ Id = _guid, Name = "Moe" , Groupid = 1};
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            
            mockCustomerService.Setup(c => c.GetCustomerInfo(It.IsAny<string>()))
                .Returns(customer);
            CustomerApiController controller = new CustomerApiController(mockCustomerService.Object,mockOrderService.Object);
            
            // Act
            var result = controller.GetCustomerInfo(_guid.ToString());
            var checkresult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(checkresult);
            Assert.AreEqual(200, checkresult.StatusCode);
            
        }
        
        [TestMethod]
        public void Should_Not_Get_NonExistent_Customer_And_Return_Correct_Status_Code()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Customer customer = null;
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            
            mockCustomerService.Setup(c => c.GetCustomerInfo(It.IsAny<string>()))
                .Returns(customer);
            CustomerApiController controller = new CustomerApiController(mockCustomerService.Object,mockOrderService.Object);
            
            // Act
            var result = controller.GetCustomerInfo(_guid.ToString());
            var checkresult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(checkresult);
            Assert.AreEqual(400, checkresult.StatusCode);
            Assert.AreEqual("Customer with Id " + _guid.ToString() + " does not exist" , checkresult.Value.ToString());
            
        }
        
        
    }
}