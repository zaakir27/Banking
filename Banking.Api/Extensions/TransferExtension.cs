using System;
using System.Collections.Generic;
using System.Linq;
using Banking.Api.Models.DataModel;
using Banking.Api.Models.Dtos;
using Account = Banking.Api.Models.Dtos.Account;

namespace Banking.Api.Extensions
{
    public static class TransferExtension
    {
        /// <summary>
        /// Map from TransferDto to Transfer
        /// </summary>
        /// <returns>Transfer</returns>
        public static Transfer MapToTransfer(this TransferDto transfer)
        {
            if (transfer.FromAccount == default || transfer.ToAccount == default) throw new ArgumentNullException(nameof(transfer));

            return new Transfer()
            {
               FromCustomerId = transfer.FromAccount.CustomerId, 
               ToCustomerId = transfer.ToAccount.CustomerId, 
               FromAccountId = transfer.FromAccount.AccountId,
               ToAccountId = transfer.ToAccount.AccountId,
               Amount = transfer.FromAccount.Amount
            };
        }
        
        /// <summary>
        /// Map from Transfer to TransferDto
        /// </summary>
        /// <returns>Collection of TransferDto</returns>
        public static ICollection<TransferDto> MapToTransferDtos(this ICollection<Transfer> transfers)
        {
            if (transfers == default || !transfers.Any()) throw new ArgumentNullException(nameof(transfers));

            var transfersDto = new List<TransferDto>();
            foreach (var transfer in transfers)
            {
                var transferDto = new TransferDto
                {
                    FromAccount = new Account()
                    {
                        CustomerId = transfer.FromCustomerId,
                        AccountId  = transfer.FromAccountId,
                        Amount     = transfer.Amount
                    },
                    ToAccount = new Account()
                    {
                        CustomerId = transfer.ToCustomerId,
                        AccountId  = transfer.ToAccountId
                    }
                };
                transfersDto.Add(transferDto);
            }
            return transfersDto;
        }
    }
}