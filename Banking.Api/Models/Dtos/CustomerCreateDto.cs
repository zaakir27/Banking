using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Banking.Api.Models.Dtos
{
    public class CustomerCreateDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        public ValidationResult Validate()
        {
            var validator = new CustomerDtoValidator();
            return validator.Validate(this);
        }
    }
    
    public class CustomerDtoValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(customer => customer.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}