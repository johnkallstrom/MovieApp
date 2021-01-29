using Microsoft.Extensions.Configuration;
using MovieApp.Web.Extensions;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class SearchService : ISearchService
    {
        private readonly IApiConfigurationService _apiConfigService;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public SearchService(
            IApiConfigurationService apiConfigService,
            IConfiguration config,
            HttpClient httpClient)
        {
            _apiConfigService = apiConfigService;
            _config = config;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<MultiSearchResults> GetMultiSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<MultiSearchResults>($"search/multi?api_key={_config["API_KEY"]}&query={query}&page={page}&include_adult=false");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            foreach (var item in data.Results)
            {
                item.Url = GetMediaUrl(item.Media_Type, item.Id);
                item.Poster_Path = ImageHelper.GetImageUrl(item.Poster_Path, PosterSizeType.W342, config);
                item.Profile_Path = ImageHelper.GetImageUrl(item.Profile_Path, ProfileSizeType.Original, config);
            }

            return data;
        }
        #endregion

        #region Private Methods
        private string GetMediaUrl(string type, int id)
        {
            string url = string.Empty;

            switch (type)
            {
                case MediaType.Movie:
                    url = $"/movie/{id}";
                    break;
                case MediaType.TV:
                    url = $"/tv/{id}";
                    break;
                case MediaType.Person:
                    url = $"/person/{id}";
                    break;
            }

            return url;
        }
        #endregion
    }
}
