using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using IO.Swagger.Models;

namespace IO.Swagger.BusinessLayer
{
    public interface IOrderManager
    {
        (HttpStatusCode, IEnumerable<Order>) GetOrders();
        Order GetOrder(string orderId);
        (HttpStatusCode,string Message) ShipOrder(string orderId);
        IEnumerable<Order> GetCustomerOrders(string customerId);
        
    }
}