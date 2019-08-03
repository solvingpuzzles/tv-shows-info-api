using System;
using Newtonsoft.Json;

namespace TvShowsScraper.Services.External.Models.TvMaze
{
    public class Image
    {
        [JsonProperty("medium")]
        public Uri Medium { get; set; }

        [JsonProperty("original")]
        public Uri Original { get; set; }
    }
}