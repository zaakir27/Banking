using System;
using System.Threading.Tasks;
using Banking.Api.Domain.Interface;
using Banking.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Banking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICustomerService _customerService;
        
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _logger          = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }
        
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(CustomerCreateDto customerCreate)
        {
            if (customerCreate == default)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogDebug("Creating customer");
                var (success, message) = await _customerService.CreateAsync(customerCreate);
                if (success)
                {
                    _logger.LogDebug($"Customer {customerCreate.Name} created successfully");
                    return Ok(new { message });
                }
                
                _logger.LogDebug($"Failed to create customer with name: {customerCreate.Name}");
                return BadRequest(new {message});
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(CreateAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                return StatusCode(500, "Failed to create customer");
            }
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                _logger.LogDebug("Getting all customers");
                var customers = await _customerService.GetAsync();
                return Ok(customers);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAllAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                return StatusCode(500, "Failed to retrieve all customers");
            }
        }
        
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                _logger.LogDebug("Getting customer");
                var customer = await _customerService.GetAsync(id);
                return Ok(customer);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                return StatusCode(500, "Failed to retrieve customer");
            }
        }
    }
}