using System;
using System.Collections.Generic;
using IO.Swagger.Data;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;

namespace IO.Swagger.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        
        public Guid CreateOrder(Order order)
        {
            return _orderRepository.Create(order);
        }

        public void CancelOrder(Order order)
        {
            UpdateOrder(order);
        }

        public Order GetOrder(string orderId)
        {
            // Validate Guid
            if (IsGuid(orderId)){
                Guid id = new Guid(orderId);
                return _orderRepository.GetById(id);
            }
            else
            {
                Order O = null;
                return O;
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            // BRING ALL RECORDS BACK
            return _orderRepository.Query(x => x.Id != null);
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
        }

        public IEnumerable<Order> GetCustomerOrders(string customerId)
        {
            return _orderRepository.Query(x => x.Customer.CustomerId == customerId);
        }
        
        private bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }

        
    }
}