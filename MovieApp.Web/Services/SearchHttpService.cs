﻿using Microsoft.Extensions.Configuration;
using MovieApp.Web.Enums;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class SearchHttpService : ISearchHttpService
    {
        private const string API_KEY = "TMDB:ApiKey";
        private const string IMAGE_URL = "TMDB:ImageBaseUrl";
        private const string PLACEHOLDER_IMAGE_URL = "TMDB:PlaceholderImageBaseUrl";

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public SearchHttpService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<KeywordResults> GetKeywordSearchAsync(string query)
        {
            var data = await _httpClient.GetFromJsonAsync<KeywordResults>($"search/keyword?api_key={_config[API_KEY]}&query={query}");

            return data;
        }

        public async Task<MediaResults> GetPeopleSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<MediaResults>($"search/person?api_key={_config[API_KEY]}&query={query}&page={page}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 200, 250));

            foreach (var media in data.Results)
            {
                media.Media_Type = MediaType.Person;
                media.Profile_Path = !string.IsNullOrEmpty(media.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, media.Profile_Path)) : placeholderUrl;
                media.Url = GetMediaUrl(media.Media_Type, media.Id);
            }

            return data;
        }

        public async Task<MediaResults> GetTVSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<MediaResults>($"search/tv?api_key={_config[API_KEY]}&query={query}&page={page}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 200, 250));

            foreach (var media in data.Results)
            {
                media.Media_Type = MediaType.TV;
                media.Poster_Path = !string.IsNullOrEmpty(media.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, media.Poster_Path)) : placeholderUrl;
                media.Url = GetMediaUrl(media.Media_Type, media.Id);
            }

            return data;
        }

        public async Task<MediaResults> GetMovieSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<MediaResults>($"search/movie?api_key={_config[API_KEY]}&query={query}&page={page}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 200, 250));

            foreach (var media in data.Results)
            {
                media.Media_Type = MediaType.Movie;
                media.Poster_Path = !string.IsNullOrEmpty(media.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, media.Poster_Path)) : placeholderUrl;
                media.Url = GetMediaUrl(media.Media_Type, media.Id);
            }

            return data;
        }

        public async Task<MediaResults> GetMultiSearchAsync(string query, int page)
        {
            var data = await _httpClient.GetFromJsonAsync<MediaResults>($"search/multi?api_key={_config[API_KEY]}&query={query}&page={page}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 200, 250));

            foreach (var media in data.Results)
            {
                media.Url = GetMediaUrl(media.Media_Type, media.Id);
                media.Poster_Path = !string.IsNullOrEmpty(media.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, media.Poster_Path)) : placeholderUrl;
                media.Profile_Path = !string.IsNullOrEmpty(media.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, media.Profile_Path)) : placeholderUrl;
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