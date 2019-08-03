using System;

namespace TvShowsApi.Data.Models
{
    public class Actor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
    }
}
