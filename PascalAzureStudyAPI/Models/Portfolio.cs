using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace PascalAzureStudyAPI.Models
{
    public class Portfolio
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "portfolioId")]
        public string PortfolioId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
