using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowsApi.Data.Models;

namespace TvShowsApi.Data
{
    public interface ITvShowsContext
    {
        Task<List<TvShow>> GetTvShowsAsync();
        Task InsertAsync(List<TvShow> shows);
    }
}