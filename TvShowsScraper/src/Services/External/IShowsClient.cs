using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowsScraper.Services.External.Models.TvMaze;

namespace TvShowsScraper.Services.External
{
    public interface IShowsClient
    {
        Task<List<Show>> GetAllAsync();
    }
}