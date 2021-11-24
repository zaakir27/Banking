using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Models.Configurations;
using Banking.Api.Models.DataModel;
using Banking.Api.Repository.Interface;
using Banking.Api.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Banking.Api.UnitOfWork
{
    public class CustomerUoW : ICustomerUoW
    {
        private readonly ILogger _logger;
        private readonly ICustomerRepository _customerRepository;
        
        public CustomerUoW(ICustomerRepository customerRepository, ILogger<CustomerUoW> logger)
        {
            _logger             = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        
        public async Task<bool> CreateAsync(Customer customer)
        {
            if (customer == default) return false;
            try
            {
                var options = new DbContextOptionsBuilder<ApiDbContext>()
                    .UseInMemoryDatabase(databaseName: "Banking")
                    .Options;

                await using (var context = new ApiDbContext(options))
                {
                    _customerRepository.CreateAsync(customer, context);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(CreateAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<ICollection<Customer>> GetAsync()
        {
            try
            {
                var options = new DbContextOptionsBuilder<ApiDbContext>()
                    .UseInMemoryDatabase(databaseName: "Banking")
                    .Options; 
                await using var context = new ApiDbContext(options);
                return await _customerRepository.GetAsync(context);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<Customer> GetAsync(int id)
        {
            try
            {
                var options = new DbContextOptionsBuilder<ApiDbContext>()
                    .UseInMemoryDatabase(databaseName: "Banking")
                    .Options; 
                await using var context = new ApiDbContext(options);
                return await _customerRepository.GetAsync(id, context);
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