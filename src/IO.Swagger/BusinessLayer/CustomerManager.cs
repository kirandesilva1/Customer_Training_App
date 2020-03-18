using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using IO.Swagger.Models;
using IO.Swagger.Services;

namespace IO.Swagger.BusinessLayer
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public CustomerManager(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }
        
        public Guid CreateCustomerProfile(Customer customer)
        {
            Guid id = _customerService.CreateCustomer(customer);

            return id;
        }

        public Customer GetCustomerProfile(string id)
        {
            Customer customer = _customerService.GetCustomerInfo(id);
            return customer;
        }

        public  IEnumerable<Customer> GetCustomers()
        {
            return _customerService.GetCustomers();;
        }
        
        public bool DeleteCustomer(string id)
        {
            Boolean result = false;
            
            try
            {
                if (CheckCustomerForOrder(id))
                {
                    Customer _customer = _customerService.GetCustomerInfo(id);
                    _customerService.DeleteCustomer(_customer);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            
            return result;
        }

        public bool CheckCustomerForOrder(string id)
        {
            bool flag = false;
            // Check if Customer exists
            Customer customer = _customerService.GetCustomerInfo(id);

            if (customer == null)
            {
                flag = false;
            }
            else if (customer.Id != Guid.Empty)
            {
                IEnumerable<Order> OrderEnumerable = _orderService.GetCustomerOrders(id);
                
                if (OrderEnumerable.Any().Equals(true))
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }

            return flag;
        }
    }
}