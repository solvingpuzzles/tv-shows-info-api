using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TvShowsScraper.Services.External.Models.TvMaze;

namespace TvShowsScraper.Services.External
{
    public class CastClient : ICastClient
    {
        private readonly HttpClient _httpClient;

        public CastClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ShowCast>> GetAllByTvShowAsync(long showId)
        {
            var response = await _httpClient.GetAsync($"shows/{showId}/cast");

            var content = await response.Content.ReadAsStringAsync();
            var cast = JsonConvert.DeserializeObject<List<ShowCast>>(content);

            return cast;
        }
    }
}
