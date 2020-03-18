using System;
using System.Collections.Generic;
using System.Net;
using IO.Swagger.Models;

namespace IO.Swagger.BusinessLayer
{
    public interface ICustomerManager
    {
         Guid CreateCustomerProfile(Customer customer);
         
         Customer GetCustomerProfile(string id);
         
         IEnumerable<Customer> GetCustomers();

         bool DeleteCustomer(string id);

         bool CheckCustomerForOrder(string id);

         //bool SetBillingInformation(Billing Info);

    }
}