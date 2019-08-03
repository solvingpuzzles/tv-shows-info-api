using System;
using Newtonsoft.Json;

namespace TvShowsScraper.Services.External.Models.TvMaze
{
    public class PreviousEpisode
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }
    }
}