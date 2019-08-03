using Newtonsoft.Json;

namespace TvShowsScraper.Services.External.Models.TvMaze
{
    public class Rating
    {
        [JsonProperty("average")]
        public double? Average { get; set; }
    }
}