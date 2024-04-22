using Newtonsoft.Json;

namespace PascalAzureStudyAPI.Models
{
    public class WorkExperience
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
    }
}
