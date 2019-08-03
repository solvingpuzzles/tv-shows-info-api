using System.Collections.Generic;

namespace TvShowsApi.Data.Models
{
    public class TvShow
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Actor> Cast { get; set; }
    }
}
