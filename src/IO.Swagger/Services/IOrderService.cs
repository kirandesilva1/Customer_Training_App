using System;
using System.Collections.Generic;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;

namespace IO.Swagger.Services
{
    public interface IOrderService
    {
        Guid CreateOrder(Order order);

        void CancelOrder(Order order);

        Order GetOrder(string orderId);
        
        IEnumerable<Order> GetOrders();

        void UpdateOrder(Order order);

        IEnumerable<Order> GetCustomerOrders(string customerId);

    }
}