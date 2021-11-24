using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Api.Models.Configurations;
using Banking.Api.Models.DataModel;
using Banking.Api.Repository.Interface;
using Banking.Api.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Banking.Api.UnitOfWork
{
    public class AccountUoW : IAccountUoW
    {
        private readonly ILogger _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        
        public AccountUoW(IAccountRepository accountRepository,ICustomerRepository customerRepository, ILogger<AccountUoW> logger)
        {
            _logger             = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountRepository  = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        
        public async Task<(bool success, string message)> CreateAsync(Account account)
        {
            if (account == default) return (false, "Cannot create an empty bank account");
            
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "Banking")
                .Options;
            
            try
            {
                await using (var context = new ApiDbContext(options))
                {
                    var customer = await context.Customers.SingleOrDefaultAsync(x => x.Id == account.CustomerId);
                    if (customer == default) return (false, "Customer does not exist");
                    _accountRepository.CreateAsync(account, context);
                    
                    await context.SaveChangesAsync();    
                }
                return (true, "Account created!");
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(CreateAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<Account> GetAsync(int id)
        {
            try
            {
                var options = new DbContextOptionsBuilder<ApiDbContext>()
                    .UseInMemoryDatabase(databaseName: "Banking")
                    .Options;

                await using var context = new ApiDbContext(options);
                return await _accountRepository.GetAsync(id, context);
                
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<(bool success, string message)> TransferAmountAsync(Transfer transfer)
        {
            if (transfer == default) return (false, "Cannot transfer an empty object");
            try
            {
                var options = new DbContextOptionsBuilder<ApiDbContext>()
                    .UseInMemoryDatabase(databaseName: "Banking")
                    .Options;

                await using (var context = new ApiDbContext(options))
                {
                    var fromCustomer =  context.Customers.FirstOrDefault(x => x.Id == transfer.FromCustomerId);
                    if (fromCustomer == default) return (false, "Sending customer does not exist");

                    var toCustomer = context.Customers.FirstOrDefault(x => x.Id == transfer.ToCustomerId);
                    if (toCustomer == default) return (false, "Recipient customer does not exist");

                    var fromAccount = context.Accounts.FirstOrDefault(x => x.Id == transfer.FromAccountId);
                    if (fromAccount == default) return (false, "Sending account does not exist");

                    var toAccount = context.Accounts.FirstOrDefault(x => x.Id == transfer.ToAccountId);
                    if (toAccount == default) return (false, "Recipient account does not exist");

                    if (fromCustomer.Id == toCustomer.Id) return (false, "Sender cannot transfer to the recipient");
                    if (fromAccount.Balance - transfer.Amount <= 0) return (false, "Insufficient funds to do transfer");

                    fromAccount.Balance -= transfer.Amount;
                    toAccount.Balance   += transfer.Amount;

                    _accountRepository.LogTransferAsync(transfer, context);
                    await context.SaveChangesAsync();
                }
                return (true, "Transfer is successful");
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(TransferAmountAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }

        public async Task<ICollection<Transfer>> GetTransferHistoryByAccountIdAsync(int accountId)
        {
            try
            {
                var options = new DbContextOptionsBuilder<ApiDbContext>()
                    .UseInMemoryDatabase(databaseName: "Banking")
                    .Options;

                await using var context = new ApiDbContext(options);
                return await context.Transfers
                    .Where(x => x.FromAccountId == accountId || x.FromAccountId == accountId)
                    .ToListAsync();

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