using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Xunit;

namespace IO.Swagger.UnitTests.Functional
{
    // NOTE: MONGO DB MUST BE RUNNING IN ORDER FOR THIS TO SUCCESSFULLY EXECUTE
    // STATUS CODE SHOULD ONLY BE RETURN FROM CONTROLLER
    // 
    
    
    public class CustomerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CustomerControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturn200ForGetCustomers()
        {
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/v1/customer");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            
            //List<Customer> customers = await response.Content.ReadAsAsync<List<Customer>>();
            
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Should_Return_Response_200_For_Get_Orders()
        {
         
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/v1/order");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
            
        }


        [Fact]
        public async Task Should_Create_Customer_And_Return_ID()
        {
            // ARRANGE
            var client = _factory.CreateClient();
            
            Customer customer = new Customer();
            customer.Name = "Mad Max";
            customer.Groupid = 1;
            customer.Numberoforders = 2;
            customer.Address = new Address()
            {
                Streetname = "200 Van Exel st",
                Zipcode = "15221-111"
            };
            
            string JSON = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");

            // ACT
            var response = await client.PostAsync("/v1/customer",httpContent);
            
            // ASSERT
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/plain; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
            
        }

        [Fact]
        public async Task Should_Return_Response_Code_400_For_Non_Existent_Customer()
        {
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/v1/customer/123");

            Assert.Equal(response.StatusCode, HttpStatusCode.BadRequest);
            
        }
        
        
        [Fact]
        [Trait("Category","Integration")]
        public async Task Should_Return_Response_Code_400_Trying_To_Delete_Non_Existent_Customer()
        {
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/v1/customer/123");

            Assert.Equal(response.StatusCode, HttpStatusCode.BadRequest);
            
        }

        [Fact]
        public async Task Should_Allow_Customer_To_Be_Deleted_If_Order_Does_Not_Exist()
        {
            var client = _factory.CreateClient();
            
            // CREATE CUSTOMER
            Customer customer = new Customer();
            customer.Name = "Mad Max" + Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            customer.Groupid = 1;
            customer.Numberoforders = 2;
            customer.Address = new Address()
            {
                Streetname = "200 Van Exel st",
                Zipcode = "2100"
            };

            string JSON = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var customerresponse = await client.PostAsync("/v1/customer",httpContent);
            
            
            customerresponse.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/plain; charset=utf-8", 
                customerresponse.Content.Headers.ContentType.ToString());

            //List<Customer> customers = await response.Content.ReadAsAsync<List<Customer>>();
            
            // Get Customer Id
            string customerId = await customerresponse.Content.ReadAsStringAsync();
            
            Assert.NotNull(customerId);
            
            
            // GET CUSTOMER
            var getCustomerResponse = await client.GetAsync("/v1/customer/" + customerId);
            Customer customerCreated = await getCustomerResponse.Content.ReadAsAsync<Customer>(); // CONVERT DATA TO CUSTOMER OBJECT
            
            // DELETE CUSTOMER
            var response = await client.DeleteAsync("/v1/customer/" + customerId);
            
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
            
        }
        
        
        
        
        [Fact]
        [Trait("Category","Integration")]
        public async Task Should_Not_Allow_Customer_To_Be_Deleted_If_Order_Exists()
        {
            var client = _factory.CreateClient();
            
           
            // CREATE CUSTOMER
            Customer customer = new Customer();
            customer.Name = "Mad Max" + Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            customer.Groupid = 1;
            customer.Numberoforders = 2;
            customer.Address = new Address()
            {
                Streetname = "200 Van Exel st",
                Zipcode = "15221-111"
            };
            
            string JSON = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var customerresponse = await client.PostAsync("/v1/customer",httpContent);
            
            
            customerresponse.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/plain; charset=utf-8", 
                customerresponse.Content.Headers.ContentType.ToString());

            //List<Customer> customers = await response.Content.ReadAsAsync<List<Customer>>();
            
            // Get Customer Id
            string customerId = await customerresponse.Content.ReadAsStringAsync();
            
            Assert.NotNull(customerId);
            
   
            
            
            // GET CUSTOMER
            var getCustomerResponse = await client.GetAsync("/v1/customer/" + customerId);
            Customer customerCreated = await getCustomerResponse.Content.ReadAsAsync<Customer>(); // CONVERT DATA TO CUSTOMER OBJECT
            

            // CREATE ORDER FOR CUSTOMER
            OrderItem item = new OrderItem();
            item.Cost = 120;
            item.Qty = 10;
            item.UnitPrice = 12;
            item.ItemName = "Talon GT";
            
            Order order = new Order();
            order.Customer = customerCreated;
            order.Description = "Test Order";
            order.Shipaddress = new Address()
            {
                Streetname = "154 court street",
                Zipcode = "15221"
            };
            order.Status = Status.Active;
            order.OrderItems = new List<OrderItem>(){item};
            
            string orderJSON = JsonConvert.SerializeObject(order);
            var orderhttpContent = new StringContent(orderJSON, Encoding.UTF8, "application/json");
            
            var orderResponse = await client.PostAsync("/v1/order/", orderhttpContent);

            orderResponse.EnsureSuccessStatusCode();
            Assert.Equal("text/plain; charset=utf-8", 
                orderResponse.Content.Headers.ContentType.ToString());

            string orderId = await orderResponse.Content.ReadAsStringAsync();
            
            Assert.NotNull(orderId);
            
            // DELETE CUSTOMER
            var response = await client.DeleteAsync("/v1/customer/" + customerId);
            
            Assert.Equal(response.StatusCode, HttpStatusCode.BadRequest);

        }
        

        [Fact]
        public async Task Should_Search_For_Customer_By_Name()
        {
            // PREP CREATE SEARCH INDEX BY FIELD NAME
            
            
            // Create Customer
            
            // Search for customer name
            
            
            
        }

        
        
    }
}