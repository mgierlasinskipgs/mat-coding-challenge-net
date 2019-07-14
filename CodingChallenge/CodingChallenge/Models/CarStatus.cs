using Newtonsoft.Json;

namespace CodingChallenge.Models
{
    public class CarStatus
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("carIndex")]
        public int CarIndex { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
