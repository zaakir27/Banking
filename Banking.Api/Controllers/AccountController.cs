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
    public class AccountController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAccountService _accountService;
        
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _logger         = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }
        
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(AccountCreateDto accountCreateDto)
        {
            if (accountCreateDto == default)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogDebug("Creating account");
                var (success, message) = await _accountService.CreateAsync(accountCreateDto);
                if (success)
                {
                    _logger.LogDebug($"Account for customer id {accountCreateDto.CustomerId} created successfully");
                    return Ok(new { message });
                }
                
                _logger.LogDebug($"Failed to account for customer with id: {accountCreateDto.CustomerId}");
                return BadRequest(new {message});
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(CreateAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                return StatusCode(500, "Failed to account");
            }
        }
        
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                _logger.LogDebug("Getting account");
                var account = await _accountService.GetAsync(id);
                return Ok(account);
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                return StatusCode(500, "Failed to retrieve account");
            }
        }
        
        [HttpPost("Transfer")]
        public async Task<IActionResult> TransferAsync(TransferDto transferDto)
        {
            if (transferDto == default)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogDebug("initiating transfer...");
                var (success, message) = await _accountService.TransferAmountAsync(transferDto);
                if (success)
                {
                    _logger.LogDebug($"Transfer from customer id {transferDto.FromAccount} to {transferDto.ToAccount} succeed");
                    return Ok(new { message });
                }
                
                _logger.LogDebug($"Transfer from customer id {transferDto.FromAccount} to {transferDto.ToAccount} was not successful");
                return BadRequest(new {message});
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(TransferAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                return StatusCode(500, "Failed to account");
            }
        }
        
        [HttpPost("GetAccountHistory/{accountId}")]
        public async Task<IActionResult> GetAccountHistoryAsync(int accountId)
        {
            try
            {
                _logger.LogDebug("Getting transfer history...");
                var result = await _accountService.GetTransferHistoryByAccountIdAsync(accountId);
                return Ok(result);
                
            }
            catch (Exception exception)
            {
                var errorMessage = $"Exception on '{nameof(GetAccountHistoryAsync)}'. Error message: '{exception.Message}'.";
                _logger.LogError(exception, errorMessage);
                return StatusCode(500, "Failed to account");
            }
        }
    }
}