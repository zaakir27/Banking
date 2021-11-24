using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Models.DataModel;

namespace Banking.Api.UnitOfWork.Interface
{
    public interface ICustomerUoW
    {
        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>A success boolean and a string message</returns>
        Task<bool> CreateAsync(Customer customer);
        
        /// <summary>
        /// Retrieves all customers
        /// </summary>
        /// <returns>A collection of customers</returns>
        Task<ICollection<Customer>> GetAsync();
        
        /// <summary>
        /// Retrieves a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A customer</returns>
        Task<Customer> GetAsync(int id);
    }
}