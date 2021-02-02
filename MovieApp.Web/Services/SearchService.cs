using Microsoft.Extensions.Configuration;
using MovieApp.Web.Enums;
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
        public async Task<SearchResults> GetPeopleSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<SearchResults>($"search/person?api_key={_config[API_KEY]}&query={query}&page={page}");

            foreach (var media in data.Results)
            {
                media.Media_Type = MediaType.Person;
                media.Profile_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.W154, media.Profile_Path);
                media.Url = GetMediaUrl(media.Media_Type, media.Id);
            }

            return data;
        }

        public async Task<SearchResults> GetTVSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<SearchResults>($"search/tv?api_key={_config[API_KEY]}&query={query}&page={page}");

            foreach (var media in data.Results)
            {
                media.Media_Type = MediaType.TV;
                media.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.W154, media.Poster_Path);
                media.Url = GetMediaUrl(media.Media_Type, media.Id);
            }

            return data;
        }

        public async Task<SearchResults> GetMovieSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<SearchResults>($"search/movie?api_key={_config[API_KEY]}&query={query}&page={page}");

            foreach (var media in data.Results)
            {
                media.Media_Type = MediaType.Movie;
                media.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.W154, media.Poster_Path);
                media.Url = GetMediaUrl(media.Media_Type, media.Id);
            }

            return data;
        }

        public async Task<SearchResults> GetMultiSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<SearchResults>($"search/multi?api_key={_config[API_KEY]}&query={query}&page={page}");

            foreach (var media in data.Results)
            {
                media.Url = GetMediaUrl(media.Media_Type, media.Id);
                media.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.W154, media.Poster_Path);
                media.Profile_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], ProfileSizeType.W185, media.Profile_Path);
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
