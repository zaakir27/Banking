using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Models.Dtos;

namespace Banking.Api.Domain.Interface
{
    public interface ICustomerService
    {
        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="customerCreateDto"></param>
        /// <returns>A success boolean and a string message</returns>
        Task<(bool success, string message)> CreateAsync(CustomerCreateDto customerCreateDto);

        /// <summary>
        /// Retrieves all customers
        /// </summary>
        /// <returns>A collection of customers</returns>
        Task<ICollection<CustomerDto>> GetAsync();

        /// <summary>
        /// Retrieves a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A customer</returns>
        Task<CustomerDto> GetAsync(int id);
    }
}