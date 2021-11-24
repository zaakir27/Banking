using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Models.Configurations;
using Banking.Api.Models.DataModel;

namespace Banking.Api.Repository.Interface
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="context"></param>
        void CreateAsync(Customer customer, ApiDbContext context);

        /// <summary>
        /// Retrieves all customers
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A collection of customers</returns>
        Task<ICollection<Customer>> GetAsync(ApiDbContext context);

        /// <summary>
        /// Retrieves a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns>A customer</returns>
        Task<Customer> GetAsync(int id, ApiDbContext context);
    }
}