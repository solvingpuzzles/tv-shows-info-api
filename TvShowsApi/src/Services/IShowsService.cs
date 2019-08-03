using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowsApi.Data.Models;

namespace TvShowsApi.Services
{
    public interface IShowsService
    {
        Task<List<TvShow>> GetTvShowsAsync(int? page);
        Task AddTvShowsAsync(List<TvShow> shows);

    }
}