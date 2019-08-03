using System.Collections.Generic;

namespace TvShowsApi.Host.Models
{
    public class PostTvShowDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<PostActorDto> Cast { get; set; }
    }
}