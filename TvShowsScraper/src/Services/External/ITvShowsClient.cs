using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowsScraper.Services.External.Models.TvShows;

namespace TvShowsScraper.Services.External
{
    public interface ITvShowsClient
    {
        Task AddTvShowsAsync(List<TvShow> shows);
    }
}