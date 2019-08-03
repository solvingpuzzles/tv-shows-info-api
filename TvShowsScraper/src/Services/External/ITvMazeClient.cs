using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowsScraper.Services.External.Models;
using TvShowsScraper.Services.External.Models.TvMaze;

namespace TvShowsScraper.Services.External
{
    public interface ITvMazeClient
    {
        Task<List<Show>> GetAllTvShowsAsync();
        Task<List<ShowCast>> GetCastByTvShowAsync(long showId);
    }
}