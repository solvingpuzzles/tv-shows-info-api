using Newtonsoft.Json;

namespace TvShowsScraper.Services.External.Models.TvMaze
{
    public class ShowCast
    {
        [JsonProperty("person")]
        public Person Person { get; set; }

        [JsonProperty("character")]
        public Character Character { get; set; }

        [JsonProperty("self")]
        public bool Self { get; set; }

        [JsonProperty("voice")]
        public bool Voice { get; set; }
    }
}
