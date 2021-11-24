using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Api.Models.Configurations;
using Banking.Api.Models.DataModel;
using Banking.Api.Repository.Interface;
using LinqToDB;
using Microsoft.Extensions.Logging;

namespace Banking.Api.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger _logger;
        
        public CustomerRepository(ILogger<CustomerRepository> logger)
        {
            _logger             = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async void CreateAsync(Customer customer, ApiDbContext context)
        {
            if (context == default) throw new ArgumentNullException(nameof(context));
            if (customer == default) throw new ArgumentNullException(nameof(customer));
            
            try
            {
               await context.Customers.AddAsync(customer);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(CreateAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<ICollection<Customer>> GetAsync(ApiDbContext context)
        {
            if (context == default) throw new ArgumentNullException(nameof(context));
            try
            {
                var data = context.Customers.OrderBy(x => x.Id);
                return await data.ToListAsync();
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<Customer> GetAsync(int id, ApiDbContext context)
        {
            if (context == default) throw new ArgumentNullException(nameof(context));
            try
            {
               return await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
    }
}