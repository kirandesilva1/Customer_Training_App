using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IO.Swagger.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IO.Swagger.UnitTests
{
    public static class TestManager
    {
        
        // CREATE CUSTOMER 
        public static async Task<string> CreateCustomer(Customer customer, WebApplicationFactory<Startup> factory)
        {
            var client = factory.CreateClient();
            
            string JSON = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(JSON, Encoding.UTF8, "application/json");
            var customerResponse = await client.PostAsync("/v1/customer",httpContent);
            
            customerResponse.EnsureSuccessStatusCode(); // Status Code 200-299
            return await customerResponse.Content.ReadAsStringAsync();
            
        }
        
        // GET CUSTOMER
        public static async Task<Customer> GetCustomer(String customerId, WebApplicationFactory<Startup> factory)
        {
            var client = factory.CreateClient();
            
            var getCustomerResponse = await client.GetAsync("/v1/customer/" + customerId);
            return await getCustomerResponse.Content.ReadAsAsync<Customer>();
        }
        

        // CREATE ORDER
        public static async Task<string> CreateOrder(Order order, WebApplicationFactory<Startup> factory)
        {
            var client = factory.CreateClient();
            
            string orderJSON = JsonConvert.SerializeObject(order);
            var orderhttpContent = new StringContent(orderJSON, Encoding.UTF8, "application/json");
            
            var orderResponse = await client.PostAsync("/v1/order/", orderhttpContent);

            orderResponse.EnsureSuccessStatusCode();
            
            return await orderResponse.Content.ReadAsStringAsync();
        }
        
        
        // GET ORDER
        public static async Task<Order> GetOrder(string orderId, WebApplicationFactory<Startup> factory)
        {
            var client = factory.CreateClient();
            
            var response = await client.GetAsync("/v1/orders/" + orderId);
            
            return await response.Content.ReadAsAsync<Order>();
            
        }
        
      
        // GET ORDERS
        public static async Task<List<Order>> GetOrders(WebApplicationFactory<Startup> factory)
        {
            var client = factory.CreateClient();
            
            var response = await client.GetAsync("/v1/orders");
            
            return await response.Content.ReadAsAsync<List<Order>>();
            
        }
        
    }
}