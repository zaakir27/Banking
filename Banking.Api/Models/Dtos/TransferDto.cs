using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Banking.Api.Models.Dtos
{
    public class TransferDto
    {
        [JsonProperty("fromAccount")] 
        public Account FromAccount { get; set; }

        [JsonProperty("toAccount")] 
        public Account ToAccount { get; set; }
        
        public ValidationResult TransferFromValidator()
        {
            var validator = new TransferToValidator();
            return validator.Validate(this);
        }
        
        public ValidationResult ValidateToAccount()
        {
            var validator = new TransferToValidator();
            return validator.Validate(this);
        }
    }

    public class Account
    {
        [JsonProperty("customerId")] 
        public int CustomerId { get; set; }

        [JsonProperty("accountId")] 
        public int AccountId { get; set; }

        [JsonProperty("accountId")] 
        public decimal Amount { get; set; }
    }
    
    public class TransferToValidator : AbstractValidator<TransferDto>
    {
        public TransferToValidator()
        {
            RuleFor(x => x.ToAccount.CustomerId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("No Customer specified");
            
            RuleFor(x => x.ToAccount.AccountId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("No account specified");
        }
    }
    
    public class TransferFromValidator : AbstractValidator<TransferDto>
    {
        public TransferFromValidator()
        {
            RuleFor(x => x.FromAccount.CustomerId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("No Customer specified");
            
            RuleFor(x => x.FromAccount.AccountId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("No account specified");
            
            RuleFor(x => x.FromAccount.Amount)
                .GreaterThanOrEqualTo(0)
                .ScalePrecision(2,15)
                .WithMessage("Invalid amount specified");
        }
    }
}