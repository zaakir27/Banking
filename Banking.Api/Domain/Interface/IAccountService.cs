using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Models.Dtos;

namespace Banking.Api.Domain.Interface
{
    public interface IAccountService
    {
        /// <summary>
        /// Creates an account
        /// </summary>
        /// <param name="accountCreateDto"></param>
        /// <returns>A success boolean and a string message</returns>
        Task<(bool success, string message)> CreateAsync(AccountCreateDto accountCreateDto);
        
        /// <summary>
        /// Retrieves an account
        /// </summary>
        /// <returns>An account</returns>
        Task<AccountViewDto> GetAsync(int id);

        /// <summary>
        /// Transfer money from one account to another
        /// </summary>
        /// <param name="transferDto"></param>
        /// <returns>A success boolean and a string message</returns>
        Task<(bool success, string message)> TransferAmountAsync(TransferDto transferDto);

        /// <summary>
        /// Retrieves a transfer history of an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>A collection of transfer histories</returns>
        Task<ICollection<TransferDto>> GetTransferHistoryByAccountIdAsync(int accountId);
    }
}