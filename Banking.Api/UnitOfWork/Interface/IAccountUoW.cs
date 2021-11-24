using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Models.DataModel;

namespace Banking.Api.UnitOfWork.Interface
{
    public interface IAccountUoW
    {
        /// <summary>
        /// Creates an account
        /// </summary>
        /// <param name="account"></param>
        /// <returns>A success boolean and a string message</returns>
        Task<(bool success, string message)> CreateAsync(Account account);
        
        /// <summary>
        /// Retrieves an account
        /// </summary>
        /// <returns>An account</returns>
        Task<Account> GetAsync(int id);
        
        /// <summary>
        /// Transfer money from one account to another
        /// </summary>
        /// <param name="transfer"></param>
        /// <returns>A success boolean and a string message</returns>
        Task<(bool success, string message)> TransferAmountAsync(Transfer transfer);
        
        /// <summary>
        /// Retrieves a transfer history of an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>A collection of transfer histories</returns>
        Task<ICollection<Transfer>> GetTransferHistoryByAccountIdAsync(int accountId);
    }
}