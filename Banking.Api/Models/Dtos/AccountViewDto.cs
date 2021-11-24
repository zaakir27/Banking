using Newtonsoft.Json;

namespace Banking.Api.Models.Dtos
{
    public class AccountViewDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
        
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
    }
}