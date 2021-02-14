﻿using Microsoft.Extensions.Configuration;
using MovieApp.Web.Enums;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class TVService : ITVService
    {
        private const string API_KEY = "ApiKey";
        private const string IMAGE_URL = "ImageBaseUrl";
        private const string PLACEHOLDER_IMAGE_URL = "PlaceholderImageBaseUrl";

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public TVService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<TVShowDetails> GetTVDetailsAsync(int tvShowId)
        {
            var data = await _httpClient.GetFromJsonAsync<TVShowDetails>($"tv/{tvShowId}?api_key={_config[API_KEY]}");

            string tvPlaceholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 1000, 1500));
            string seasonPlaceholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            data.Poster_Path = !string.IsNullOrEmpty(data.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.Original, data.Poster_Path)) : tvPlaceholderUrl;

            foreach (var season in data.Seasons)
            {
                season.Poster_Path = !string.IsNullOrEmpty(season.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, season.Poster_Path)) : seasonPlaceholderUrl;
            }

            return data;
        }

        public async Task<IEnumerable<TVShow>> GetPopularTVAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/popular?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = !string.IsNullOrEmpty(tvShow.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, tvShow.Poster_Path)) : placeholderUrl;
            }

            return data.Results;
        }

        public async Task<IEnumerable<TVShow>> GetTopRatedTVAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/top_rated?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = !string.IsNullOrEmpty(tvShow.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, tvShow.Poster_Path)) : placeholderUrl;
            }

            return data.Results;
        }

        public async Task<TVResults> GetOnTheAirTVAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/on_the_air?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = !string.IsNullOrEmpty(tvShow.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, tvShow.Poster_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<TVResults> GetOnTheAirTVAsync(int page)
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/on_the_air?api_key={_config[API_KEY]}&page={page}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = !string.IsNullOrEmpty(tvShow.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, tvShow.Poster_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<IEnumerable<Person>> GetTVCastAsync(int tvShowId)
        {
            var data = await _httpClient.GetFromJsonAsync<TVCredits>($"tv/{tvShowId}/credits?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var person in data.Cast)
            {
                person.Profile_Path = !string.IsNullOrEmpty(person.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, person.Profile_Path)) : placeholderUrl;
            }

            return data.Cast;
        }

        public async Task<SeasonDetails> GetTVSeasonDetailsAsync(int tvShowId, int seasonNumber)
        {
            var data = await _httpClient.GetFromJsonAsync<SeasonDetails>($"tv/{tvShowId}/season/{seasonNumber}?api_key={_config[API_KEY]}");

            string seasonPlaceholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));
            string episodePlaceholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 300, 170));

            data.Poster_Path = !string.IsNullOrEmpty(data.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, data.Poster_Path)) : seasonPlaceholderUrl;

            foreach (var episode in data.Episodes)
            {
                episode.Still_Path = !string.IsNullOrEmpty(episode.Still_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], StillSizeType.W300, episode.Still_Path)) : episodePlaceholderUrl;
            }

            return data;
        }
        #endregion
    }
}
