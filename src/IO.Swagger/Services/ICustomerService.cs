using System;
using System.Collections.Generic;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;

namespace IO.Swagger.Services
{
    public interface ICustomerService
    {
        Guid CreateCustomer(Customer customer);

        void DeleteCustomer(Customer customer);

        Customer GetCustomerInfo(string customerId);

        IEnumerable<Customer> GetCustomers();

        void UpdateCustomer(Customer customer);
        
    }
}