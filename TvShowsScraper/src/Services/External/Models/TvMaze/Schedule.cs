using System.Collections.Generic;
using Newtonsoft.Json;

namespace TvShowsScraper.Services.External.Models.TvMaze
{
    public class Schedule
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("days")]
        public List<string> Days { get; set; }
    }
}