using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IO.Swagger.Models;
using IO.Swagger.UnitTests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IO.Swagger.IntegrationTests
{
    public class OrderControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public OrderControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task Should_Return_For_GetOrders()
        {
            // Act

            for (int i = 0; i < 4; i++)
            {
                // CREATE ORDER
                OrderItem item = new OrderItem();
                item.Cost = 120;
                item.Qty = 10;
                item.UnitPrice = 12;
                item.ItemName = "Talon GT";
            
                Order order = new Order();
                order.Customer = new Customer(){ Name = "Ktest-" + i};
                order.Description = "Test Order";
                order.Shipaddress = new Address()
                {
                    Streetname = "154 court street",
                    Zipcode = "15221"
                };
                order.Status = Status.Active;
                order.OrderItems = new List<OrderItem>(){item};

                string orderId = await TestManager.CreateOrder(order, _factory);

            }

            List<Order> orders = await TestManager.GetOrders(_factory);

            Assert.NotNull(orders);
            
        }
        
        [Fact]
        public async Task Should_Not_Send_Order_If_Billing_Info_Is_Not_Available()
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
            
            string customerId = await customerresponse.Content.ReadAsStringAsync();
            
            Assert.NotNull(customerId);
            
            // GET CUSTOMER
            var getCustomerResponse = await client.GetAsync("/v1/customer/" + customerId);
            Customer customerCreated = await getCustomerResponse.Content.ReadAsAsync<Customer>(); // CONVERT DATA TO CUSTOMER OBJECT
            
            // CREATE ORDER + ORDER ITEM
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
            
            // SHIP ORDER
            string shipOrderJSON = JsonConvert.SerializeObject(orderId);
            var shipOrderhttpContent = new StringContent(shipOrderJSON, Encoding.UTF8, "application/json");
            
            var shipOrderResponse = await client.PostAsync("/v1/shiporder/", shipOrderhttpContent);
            
            Assert.Equal("text/plain; charset=utf-8", 
                shipOrderResponse.Content.Headers.ContentType.ToString());
            
            
            // SHOULD FAIL DUE TO VALIDATION
            // TODO: Change Status Code written for particular error
            Assert.Equal(HttpStatusCode.Forbidden,shipOrderResponse.StatusCode); 
            
        }

        [Fact]
        public async Task Should_Get_Order_By_Id()
        {
            // CREATE CUSTOMER
            Customer customer = new Customer();
            customer.Name = "Test Customer-" + Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            customer.Groupid = 1;
            customer.Numberoforders = 2;
            customer.Address = new Address()
            {
                Streetname = "200 Van Exel st",
                Zipcode = "15221-111"
            };
            customer.Billing = new BillingInfo()
            {
                CreditCardNumber = "xxx-xxxx-xxx-xxxx",
                BillinginfoId = "x121324"
            };

            string customerId = await TestManager.CreateCustomer(customer, _factory);
            
            Assert.NotNull(customerId);
            
            Customer customerCreated = await TestManager.GetCustomer(customerId, _factory);
            
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

            string orderId = await TestManager.CreateOrder(order, _factory);

            Order _order = await TestManager.GetOrder(orderId, _factory);
            
            Assert.Equal(orderId,_order.OrderId); 
            
        }
        
    }
}