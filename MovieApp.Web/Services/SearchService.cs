using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class SearchService : ISearchService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationService _configService;
        private readonly HttpClient _httpClient;

        public SearchService(
            IConfiguration config,
            IConfigurationService configService,
            HttpClient httpClient)
        {
            _config = config;
            _configService = configService;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Media>> GetMultiSearchAsync(SearchParameters parameters)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"search/multi?api_key={_config["API_KEY"]}&query={parameters.Query}&page={parameters.Page}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var data = JsonSerializer.Deserialize<SearchResults>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var imageConfig = await GetImageConfiguration();

            foreach (var item in data.Results)
            {
                item.Poster_Path = ImageHelper.GetImageUrl(item.Poster_Path, PosterSizeType.W342, imageConfig);
                item.Profile_Path = ImageHelper.GetImageUrl(item.Profile_Path, PosterSizeType.W342, imageConfig);
            }

            return data.Results;
        }

        #region Private Methods
        private async Task<Image> GetImageConfiguration()
        {
            var configuration = await _configService.GetApiConfigurationAsync();

            return configuration.Images;
        }
        #endregion
    }
}
