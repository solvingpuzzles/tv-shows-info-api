using Newtonsoft.Json;

namespace TvShowsScraper.Services.External.Models.TvMaze
{
    public class Externals
    {
        [JsonProperty("tvrage")]
        public long? Tvrage { get; set; }

        [JsonProperty("thetvdb")]
        public long? Thetvdb { get; set; }

        [JsonProperty("imdb")]
        public string Imdb { get; set; }
    }
}