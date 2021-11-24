using System.Threading.Tasks;
using Banking.Api.Models.Configurations;
using Banking.Api.Models.DataModel;

namespace Banking.Api.Repository.Interface
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Creates an account
        /// </summary>
        /// <param name="account"></param>
        /// <param name="context"></param>
        void CreateAsync(Account account, ApiDbContext context);
        
        /// <summary>
        /// Retrieves an account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns>An account</returns>
        Task<Account> GetAsync(int id, ApiDbContext context);
        
        /// <summary>
        /// Logs transfer details
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="context"></param>
        void LogTransferAsync(Transfer transfer, ApiDbContext context);
    }
}