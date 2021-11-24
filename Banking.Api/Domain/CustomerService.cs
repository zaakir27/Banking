using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Domain.Interface;
using Banking.Api.Extensions;
using Banking.Api.Models.Dtos;
using Banking.Api.UnitOfWork.Interface;
using Microsoft.Extensions.Logging;

namespace Banking.Api.Domain
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger _logger;
        private readonly ICustomerUoW _customerUoW;
        
        public CustomerService(ICustomerUoW customerUoW ,ILogger<CustomerService> logger)
        {
            _logger      = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerUoW = customerUoW ?? throw new ArgumentNullException(nameof(customerUoW));
        }
        
        public async Task<(bool success, string message)> CreateAsync(CustomerCreateDto customerDto)
        {
            if (customerDto == default) return (false, "Cannot create an empty customer");
            
            var validationResult = customerDto.Validate();
            if (!validationResult.IsValid)
            {
                var errorMessage = $"Validation has failed.";
                _logger.LogWarning($"{errorMessage}\nErrors: {{@ValidationErrors}}", validationResult.Errors);
                return (false, errorMessage);
            }
            
            try
            {
                var customer = customerDto.MapToCustomer();
                var result   = await _customerUoW.CreateAsync(customer);
                return !result ? (false,"Error occured while trying to create a customer") : (true,"Customer Created!");
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(CreateAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<ICollection<CustomerDto>> GetAsync()
        {
            try
            {
                var customers   = await _customerUoW.GetAsync();
                return customers?.MapToCustomerDtos();
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<CustomerDto> GetAsync(int id)
        {
            try
            {
                var customers = await _customerUoW.GetAsync(id);
                return customers?.MapToCustomerDto();
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