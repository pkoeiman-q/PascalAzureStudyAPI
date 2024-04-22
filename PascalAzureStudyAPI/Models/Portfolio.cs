using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace PascalAzureStudyAPI.Models
{
    public class Portfolio
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        // 'portfolioId' is the partition key within azure cosmos DB
        // When creating a portfolio, set this ID to be equal to the user ID
        [JsonProperty(PropertyName = "portfolioId")]
        public string PortfolioId { get; set; }

        [JsonProperty(PropertyName = "experiences")]
        public IEnumerable<WorkExperience> Experiences { get; set; }
    }
}
