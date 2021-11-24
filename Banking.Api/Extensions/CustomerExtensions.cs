using System;
using System.Collections.Generic;
using System.Linq;
using Banking.Api.Models.DataModel;
using Banking.Api.Models.Dtos;

namespace Banking.Api.Extensions
{
    public static class CustomerExtensions
    {
        /// <summary>
        /// Map from CustomerDto to Customer
        /// </summary>
        /// <returns>Customer</returns>
        public static Customer MapToCustomer(this CustomerCreateDto customerCreateDto)
        {
            if (customerCreateDto == default ) throw new ArgumentNullException(nameof(customerCreateDto));

            return new Customer()
            {
                Name          = customerCreateDto.Name
            };
        }
        
        /// <summary>
        /// Map from CustomerDto to Customer
        /// </summary>
        /// <returns>CustomerDto</returns>
        public static CustomerDto MapToCustomerDto(this Customer customer)
        {
            if (customer == default ) throw new ArgumentNullException(nameof(customer));

            return new CustomerDto()
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }
        
        /// <summary>
        /// Map from Customers to CustomerDtos
        /// </summary>
        /// <returns>A list of customerDtos</returns>
        public static ICollection<CustomerDto> MapToCustomerDtos(this ICollection<Customer> customers)
        {
            if (customers == default || !customers.Any()) throw new ArgumentNullException(nameof(customers));

            return customers
                .Select(customer => new CustomerDto 
                { 
                    Id   = customer.Id, 
                    Name = customer.Name
                })
                .ToList();
        }
    }
}