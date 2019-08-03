using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TvShowsScraper.Services.External;
using TvShowsScraper.Services.External.Models.TvMaze;
using TvShowsScraper.Services.External.Models.TvShows;

namespace TvShowsScraper.Services
{
    public class ImportService : IImportService
    {
        private readonly IScrapeService _scrapeService;
        private readonly ITvShowsClient _tvShowsClient;
        private readonly ILogger<ImportService> _logger;

        public ImportService(IScrapeService scrapeService, ITvShowsClient tvShowsClient, ILogger<ImportService> logger)
        {
            _scrapeService = scrapeService;
            _tvShowsClient = tvShowsClient;
            _logger = logger;
        }

        public async Task ImportTvShows()
        {
            var scrapedShows = await _scrapeService.ScrapeTvShowsAsync();
            var showsCounter = 0;
            var shows = scrapedShows.Select(CastTvShow).ToList();

            while (showsCounter < shows.Count)
            {
                var show = shows[showsCounter];
                var scrapedCast = await _scrapeService.ScrapeCastByTvShowAsync(show.Id);

                show.Cast = ConvertCast(scrapedCast);
                showsCounter++;
            }

            await _tvShowsClient.AddTvShowsAsync(shows);
        }
        
        private TvShow CastTvShow(Show scrapedShow)
        {
            return new TvShow
            {
                Id = scrapedShow.Id,
                Name = scrapedShow.Name
            };
        }

        private List<Actor> ConvertCast(List<ShowCast> cast)
        {
            return cast.Select(c => new Actor
            {
                Id = c.Person.Id,
                Name = c.Person.Name,
                DateOfBirth = c.Person.Birthday
            }).ToList();
        }

    }
}
