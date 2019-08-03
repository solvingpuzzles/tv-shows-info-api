using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TvShowsScraper.Services.External.Models.TvMaze;

namespace TvShowsScraper.Services.External
{
    public class TvMazeClient : ITvMazeClient
    {
        private readonly HttpClient _httpClient;

        public TvMazeClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<Show>> GetAllTvShowsAsync()
        {
            var response = await _httpClient.GetAsync("shows");
            var content = await response.Content.ReadAsStringAsync();
            var shows = JsonConvert.DeserializeObject<List<Show>>(content);

            return shows;
        }
        
        public async Task<List<ShowCast>> GetCastByTvShowAsync(long showId)
        {
            var response = await _httpClient.GetAsync($"shows/{showId}/cast");

            var content = await response.Content.ReadAsStringAsync();
            var cast = JsonConvert.DeserializeObject<List<ShowCast>>(content);

            return cast;
        }
    }
}