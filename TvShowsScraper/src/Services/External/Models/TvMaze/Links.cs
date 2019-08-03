using Newtonsoft.Json;

namespace TvShowsScraper.Services.External.Models.TvMaze
{
    public class Links
    {
        [JsonProperty("self")]
        public PreviousEpisode Self { get; set; }

        [JsonProperty("previousepisode")]
        public PreviousEpisode Previousepisode { get; set; }
    }
}