using System;
using Newtonsoft.Json;

namespace TvShowsScraper.Services.External.Models.TvMaze
{
    public class Character
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }
}