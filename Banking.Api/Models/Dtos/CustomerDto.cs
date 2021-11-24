using Newtonsoft.Json;

namespace Banking.Api.Models.Dtos
{
    public class CustomerDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}