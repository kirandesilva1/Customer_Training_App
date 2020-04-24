using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using IO.Swagger.Models;

namespace IO.Swagger.BusinessLayer
{
    public interface IOrderManager
    {
        (HttpStatusCode, IEnumerable<Order>) GetOrders();
        (HttpStatusCode, Order order) GetOrder(string orderId);
        (HttpStatusCode,string Message) ShipOrder(string orderId);
    }
}