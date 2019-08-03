using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowsApi.Data.Models;

namespace TvShowsApi.Data
{
    public interface ITvShowsContext
    {
        Task InsertAsync(List<TvShow> shows);
        Task<List<TvShow>> GetTvShowsAsync();
        Task<List<TvShow>> GetTvShowsPaginatedAsync(int page);
    }
}
