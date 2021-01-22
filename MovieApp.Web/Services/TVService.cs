using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class TVService : ITVService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationService _configService;
        private readonly HttpClient _httpClient;

        public TVService(
            IConfiguration config,
            IConfigurationService configService,
            HttpClient httpClient)
        {
            _config = config;
            _configService = configService;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TVShow>> GetTopRatedTVShowsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"tv/top_rated?api_key={_config["API_KEY"]}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var data = JsonSerializer.Deserialize<TVResults>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var tvShows = data.Results;

            var configuration = await _configService.GetApiConfigurationAsync();

            foreach (var tvShow in tvShows)
            {
                tvShow.Poster_Path = ImageHelper.GetImageUrl(tvShow.Poster_Path, PosterSizeType.Original, configuration.Images);
            }

            return tvShows;
        }
    }
}
