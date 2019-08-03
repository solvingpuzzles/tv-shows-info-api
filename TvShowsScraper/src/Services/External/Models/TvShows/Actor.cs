using System;

namespace TvShowsScraper.Services.External.Models.TvShows
{
    public class Actor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
    }
}
