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
    public class AccountService : IAccountService
    {
        private readonly ILogger _logger;
        private readonly IAccountUoW _accountUoW;
        private readonly ICustomerUoW _customerUoW;
        
        public AccountService(IAccountUoW accountUoW, ICustomerUoW customerUoW ,ILogger<AccountService> logger)
        {
            _logger      = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountUoW  = accountUoW ?? throw new ArgumentNullException(nameof(accountUoW));
            _customerUoW = customerUoW ?? throw new ArgumentNullException(nameof(customerUoW));
        }
        
        public async Task<(bool success, string message)> CreateAsync(AccountCreateDto accountCreateDto)
        {
            if (accountCreateDto == default) return (false, "Cannot create an empty bank account");
            
            var validationResult = accountCreateDto.Validate();
            if (!validationResult.IsValid)
            {
                var errorMessage = $"Validation has failed.";
                _logger.LogWarning($"{errorMessage}\nErrors: {{@ValidationErrors}}", validationResult.Errors);
                return (false, errorMessage );
            }
            
            try
            {
                var customer = await _customerUoW.GetAsync(accountCreateDto.CustomerId);
                if (customer == default) return (false, "Customer does not exist");
                
                var account = accountCreateDto.MapToAccount();
                var (success, message) = await _accountUoW.CreateAsync(account);
                return !success ? (false, message) : (true, message);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(CreateAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        
        
        public async Task<AccountViewDto> GetAsync(int id)
        {
            try
            {
                var account = await _accountUoW.GetAsync(id);
                return account?.MapToAccountDto();
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<(bool success, string message)> TransferAmountAsync(TransferDto transferDto)
        {
            if (transferDto == default) return (false, "Cannot create an empty bank account");
            
            var validationToResult = transferDto.ValidateToAccount();
            if (!validationToResult.IsValid)
            {
                var errorMessage = $"Validation has failed.";
                _logger.LogWarning($"{errorMessage}\nErrors: {{@ValidationErrors}}", validationToResult.Errors);
                return (false, errorMessage );
            }
            
            var validationFromResult = transferDto.TransferFromValidator();
            if (!validationFromResult.IsValid)
            {
                var errorMessage = $"Validation has failed.";
                _logger.LogWarning($"{errorMessage}\nErrors: {{@ValidationErrors}}", validationFromResult.Errors);
                return (false, errorMessage );
            }
            
            try
            {
                var transfer = transferDto.MapToTransfer();
                var (success, message) = await _accountUoW.TransferAmountAsync(transfer);
                return !success ? (false, message) : (true, message);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(TransferAmountAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<ICollection<TransferDto>> GetTransferHistoryByAccountIdAsync(int accountId)
        {
            try
            {
                var account = await _accountUoW.GetTransferHistoryByAccountIdAsync(accountId);
                return account?.MapToTransferDtos();
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetTransferHistoryByAccountIdAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
    }
}