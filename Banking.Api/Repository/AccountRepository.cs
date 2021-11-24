using System;
using System.Threading.Tasks;
using Banking.Api.Models.Configurations;
using Banking.Api.Models.DataModel;
using Banking.Api.Repository.Interface;
using LinqToDB;
using Microsoft.Extensions.Logging;

namespace Banking.Api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger _logger;
        
        public AccountRepository(ILogger<AccountRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async void CreateAsync(Account account, ApiDbContext context)
        {
            if (context == default) throw new ArgumentNullException(nameof(context));
            if (account == default) throw new ArgumentNullException(nameof(account));
            
            try
            {
                await context.Accounts.AddAsync(account);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(CreateAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async Task<Account> GetAsync(int id, ApiDbContext context)
        {
            if (context == default) throw new ArgumentNullException(nameof(context));
            try
            {
                return await context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
        
        public async void LogTransferAsync(Transfer transfer, ApiDbContext context)
        {
            if (context == default) throw new ArgumentNullException(nameof(context));
            if (transfer == default) throw new ArgumentNullException(nameof(transfer));
            
            try
            {
                await context.Transfers.AddAsync(transfer);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(LogTransferAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                throw new Exception(errorMessage, exception);
            }
        }
    }
}