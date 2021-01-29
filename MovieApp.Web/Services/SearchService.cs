using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class SearchService : ISearchService
    {
        private const string API_KEY = "API_KEY";
        private const string IMAGE_BASE_URL = "IMAGE_BASE_URL";

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
        public async Task<SearchResults> GetMultiSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<SearchResults>($"search/multi?api_key={_config[API_KEY]}&query={query}&page={page}&include_adult=false");

            foreach (var item in data.Results)
            {
                item.Url = GetMediaUrl(item.Media_Type, item.Id);
                item.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.W342, item.Poster_Path);
                item.Profile_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], ProfileSizeType.Original, item.Profile_Path);
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
                    url = $"/tv-shows/{id}";
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
