using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TvShowsScraper.Services.External.Models.TvShows
{
    public class TvShows
    {
        public List<TvShow> Shows { get; set; }
    }
}
