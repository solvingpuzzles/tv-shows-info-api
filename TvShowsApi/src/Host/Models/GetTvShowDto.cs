using System.Collections.Generic;

namespace TvShowsApi.Host.Models
{
    public class GetTvShowDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<GetActorDto> Cast { get; set; }
    }
}
