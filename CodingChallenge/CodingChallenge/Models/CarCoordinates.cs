using Newtonsoft.Json;

namespace CodingChallenge.Models
{
    public class CarCoordinates
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("carIndex")]
        public int CarIndex { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }
    }
}
