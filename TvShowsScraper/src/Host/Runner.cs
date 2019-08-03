using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TvShowsScraper.Services;

namespace TvShowsScraper.Host
{
    public sealed class Runner
    {
        private readonly IImportService _importService;
        private readonly ILogger<Runner> _logger;

        public Runner(IImportService importService, ILogger<Runner> logger)
        {
            _importService = importService;
            _logger = logger;
        }

        public async Task Run()
        {
            try
            {
                _logger.LogInformation("### SCRAPE SERVICE STARTED.");
                
                await _importService.ImportTvShows();
                
                _logger.LogInformation("### SCRAPE SERVICE STOPPED.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "SCRAPE SERVICE THREW AN EXCEPTION.");
            }
        }
    }
}