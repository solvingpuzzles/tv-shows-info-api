using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TvShowsScraper.Services.External.Models.TvShows;

namespace TvShowsScraper.Services.External
{
    public class TvShowsClient : ITvShowsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TvShowsClient> _logger;

        public TvShowsClient(HttpClient httpClient, ILogger<TvShowsClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task AddTvShowsAsync(List<TvShow> shows)
        {
            var tvShows = new TvShows { Shows = shows };
            var json = JsonConvert.SerializeObject(tvShows, Formatting.None);
            var content = new StringContent(
                JsonConvert.SerializeObject(tvShows, Formatting.None),
                Encoding.UTF8, 
                "application/json");
            var result = await _httpClient.PostAsync("api/shows", content);

            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("TV Shows successfully sent to API.");
            }
            else
            {
                _logger.LogError("Error while sending TV Shows to API.", result.StatusCode, result.ReasonPhrase);
            }
        }
    }
}
