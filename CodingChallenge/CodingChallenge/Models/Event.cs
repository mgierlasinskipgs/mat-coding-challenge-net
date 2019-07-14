using Newtonsoft.Json;

namespace CodingChallenge.Models
{
    public class Event
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
