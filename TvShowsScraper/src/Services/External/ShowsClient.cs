using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TvShowsScraper.Services.External.Models.TvMaze;

namespace TvShowsScraper.Services.External
{
    public class ShowsClient : IShowsClient
    {
        private readonly HttpClient _httpClient;

        public ShowsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<Show>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("shows");
            var content = await response.Content.ReadAsStringAsync();
            var shows = JsonConvert.DeserializeObject<List<Show>>(content);

            return shows;
        }
    }
}
