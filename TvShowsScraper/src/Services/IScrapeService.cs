using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowsScraper.Services.External.Models.TvMaze;

namespace TvShowsScraper.Services
{
    public interface IScrapeService
    {
        Task<List<Show>> ScrapeTvShowsAsync();
        Task<List<ShowCast>> ScrapeCastByTvShowAsync(long showId);
    }
}