using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Banking.Api.Models.Dtos
{
    public class AccountCreateDto
    {
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
        
        [JsonProperty("deposit")]
        public decimal Deposit { get; set; }
        
        public ValidationResult Validate()
        {
            var validator = new AccountCreateDtoValidator();
            return validator.Validate(this);
        }
    }
    
    public class AccountCreateDtoValidator : AbstractValidator<AccountCreateDto>
    {
        public AccountCreateDtoValidator()
        {
            RuleFor(account => account.Deposit)
                .GreaterThanOrEqualTo(0)
                .ScalePrecision(2,15)
                .WithMessage("Invalid deposit amount");
        }
    }
}