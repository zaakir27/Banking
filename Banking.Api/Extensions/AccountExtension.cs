using System;
using Banking.Api.Models.Dtos;
using Account = Banking.Api.Models.DataModel.Account;

namespace Banking.Api.Extensions
{
    public static class AccountExtension
    {
        /// <summary>
        /// Map from accountCreateDto to Account
        /// </summary>
        /// <returns>Customer</returns>
        public static Account MapToAccount(this AccountCreateDto accountCreateDto)
        {
            if (accountCreateDto == default ) throw new ArgumentNullException(nameof(accountCreateDto));

            return new Account()
            {
                CustomerId = accountCreateDto.CustomerId,
                Balance = accountCreateDto.Deposit
            };
        }
        
        /// <summary>
        /// Map from Account to AccountDto
        /// </summary>
        /// <returns>AccountDto</returns>
        public static AccountViewDto MapToAccountDto(this Account account)
        {
            if (account == default ) throw new ArgumentNullException(nameof(account));

            return new AccountViewDto()
            {
                Id = account.Id, 
                CustomerId = account.CustomerId, 
                Balance = account.Balance,
            };
        }
    }
}