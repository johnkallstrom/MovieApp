using Microsoft.Extensions.Configuration;
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
        private const string API_KEY = "API_KEY";
        private const string IMAGE_BASE_URL = "IMAGE_BASE_URL";

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

            data.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.Original, data.Poster_Path);

            foreach (var season in data.Seasons)
            {
                season.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.W500, season.Poster_Path);
            }

            return data;
        }

        public async Task<IEnumerable<TVShow>> GetPopularTVAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/popular?api_key={_config[API_KEY]}");

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.Original, tvShow.Poster_Path);
            }

            return data.Results;
        }

        public async Task<IEnumerable<TVShow>> GetTopRatedTVAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/top_rated?api_key={_config[API_KEY]}");

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.Original, tvShow.Poster_Path);
            }

            return data.Results;
        }

        public async Task<IEnumerable<TVShow>> GetOnTheAirTVAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/on_the_air?api_key={_config[API_KEY]}");

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.Original, tvShow.Poster_Path);
            }

            return data.Results;
        }

        public async Task<IEnumerable<Person>> GetTVCastAsync(int tvShowId)
        {
            var data = await _httpClient.GetFromJsonAsync<TVCredits>($"tv/{tvShowId}/credits?api_key={_config[API_KEY]}");

            foreach (var person in data.Cast)
            {
                person.Profile_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.W342, person.Profile_Path);
            }

            return data.Cast;
        }

        public async Task<SeasonDetails> GetTVSeasonDetailsAsync(int tvShowId, int seasonNumber)
        {
            var data = await _httpClient.GetFromJsonAsync<SeasonDetails>($"tv/{tvShowId}/season/{seasonNumber}?api_key={_config[API_KEY]}");

            data.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.Original, data.Poster_Path);

            foreach (var episode in data.Episodes)
            {
                episode.Still_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], StillSizeType.W300, episode.Still_Path);
            }

            return data;
        }
        #endregion
    }
}
