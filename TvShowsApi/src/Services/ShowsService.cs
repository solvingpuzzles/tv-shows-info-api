using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TvShowsApi.Data;
using TvShowsApi.Data.Models;

namespace TvShowsApi.Services
{
    public class ShowsService : IShowsService
    {
        private readonly ITvShowsContext _context;
        private readonly ILogger<ShowsService> _logger;

        public ShowsService(ITvShowsContext context, ILogger<ShowsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TvShow>> GetTvShowsAsync(int? page)
        {
            if (page.HasValue)
            {
                _logger.LogDebug($"Requested TV Shows with pagination enabled. Page requested: {page.Value}.");
            }

            _logger.LogDebug($"Requested all TV Shows.");
            return await _context.GetTvShowsAsync();
        }

        public async Task AddTvShowsAsync(List<TvShow> shows)
        {
            if (shows == null || !shows.Any())
            {
                _logger.LogInformation("No TV Shows to insert.");
                return;
            }
            
            try
            {
                var removedAll = await RemoveDuplicates(shows);
                if (removedAll)
                {
                    _logger.LogInformation("After removing duplicates, there was no TV Show to insert.");
                    return;
                }

                _logger.LogDebug($"");
                await _context.InsertAsync(shows);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "");
                throw;
            }
        }

        private async Task<bool> RemoveDuplicates(List<TvShow> shows)
        {
            var existingShows = await _context.GetTvShowsAsync();

            shows.RemoveAll(x => existingShows.Exists(y => y.Id == x.Id));

            return !shows.Any();
        }
    }
}
