using System;

namespace TvShowsApi.Host.Models
{
    public class PostActorDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
    }
}
