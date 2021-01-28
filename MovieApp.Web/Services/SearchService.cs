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
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public SearchService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<MultiSearchResults> GetMultiSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<MultiSearchResults>($"search/multi?api_key={_config["API_KEY"]}&query={query}&page={page}");

            var imageConfig = await GetImageConfiguration();

            foreach (var item in data.Results)
            {
                item.Url = GetMediaUrl(item.Media_Type, item.Id);
                item.Poster_Path = ImageHelper.GetImageUrl(item.Poster_Path, FileSizeType.W342, imageConfig);
                item.Profile_Path = ImageHelper.GetImageUrl(item.Profile_Path, FileSizeType.W342, imageConfig);
            }

            return data;
        }
        #endregion

        #region Private Methods
        private async Task<Image> GetImageConfiguration()
        {
            var configuration = await _httpClient.GetFromJsonAsync<Configuration>($"configuration?api_key={_config["API_KEY"]}");

            return configuration.Images;
        }

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
