using Newtonsoft.Json;

namespace CodingChallenge.Models
{
    public class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("long")]
        public double Long { get; set; }
    }

}
