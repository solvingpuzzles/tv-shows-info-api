using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TvShowsScraper.Services.External;
using TvShowsScraper.Services.External.Models.TvMaze;

namespace TvShowsScraper.Services
{
    public class ScrapeService : IScrapeService
    {
        private readonly ITvMazeClient _tvMazeClient;
        private readonly ILogger<ScrapeService> _logger;

        public ScrapeService(ITvMazeClient tvMazeClient, ILogger<ScrapeService> logger)
        {
            _tvMazeClient = tvMazeClient;
            _logger = logger;
        }
        
        public async Task<List<Show>> ScrapeTvShowsAsync()
        {
            return await _tvMazeClient.GetAllTvShowsAsync();
        }

        public async Task<List<ShowCast>> ScrapeCastByTvShowAsync(long showId)
        {
            return await _tvMazeClient.GetCastByTvShowAsync(showId);
        }
    }
}
