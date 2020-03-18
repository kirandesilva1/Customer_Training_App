using System;
using System.Collections.Generic;
using IO.Swagger.Data;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace IO.Swagger.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        
        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        public Guid CreateCustomer(Customer customer)
        {
           
           return _customerRepository.Create(customer);
            
        }

        public void DeleteCustomer(Customer customer)
        {
            _customerRepository.Delete(customer);
        }

        public Customer GetCustomerInfo(string customerId)
        {
            // Validate Guid
            if (IsGuid(customerId)){
                Guid id = new Guid(customerId);
                return _customerRepository.GetById(id);
            }
            else
            {
                Customer c = null;
                return c;
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            // BRING ALL RECORDS BACK
            return _customerRepository.Query(x => x.Id != null);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        private bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }
        
    }
}