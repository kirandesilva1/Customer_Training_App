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
    public class CustomerTests
    {
        [TestMethod]
        public void Should_Create_Customer_Return_Success_ResponseCode_And_Guid()
        {
            // Arrange
            Guid _guid = Guid.NewGuid();
            Customer customer = new Customer(){ Name = "Moe" , Groupid = 1};
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();
            mockCustomerService.Setup(c => c.CreateCustomer(It.IsAny<Customer>()))
                .Returns(_guid);
            CustomerApiController controller = new CustomerApiController(mockCustomerService.Object,mockOrderService.Object);
            
            // Act
            var result = controller.CreateCustomer(customer);
            var checkresult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(checkresult);
            Assert.AreEqual(_guid.ToString(),checkresult.Value.ToString());
            Assert.AreEqual(200, checkresult.StatusCode);
            
        }
        
        //TODO: Need a way to validate required fields
        /*[TestMethod]
        public void Should_Not_Create_Customer_Without_Required_Fields_Set()
        {
            // Arrange
            Customer customer = new Customer();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            CustomerApiController controller = new CustomerApiController(mockCustomerService.Object);

            // Act
            var result = controller.TryValidateModel(customer);
            
            // Assert
            Assert.IsTrue(result == true);
            
        }*/
        
    }
}