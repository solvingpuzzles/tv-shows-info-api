using System;
using Newtonsoft.Json;

namespace TvShowsScraper.Services.External.Models.TvMaze
{
    public partial class Self
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }
    }
}