using System.Collections.Generic;

namespace TvShowsScraper.Services.External.Models.TvShows
{
    public class TvShow
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Actor> Cast { get; set; }
    }
}