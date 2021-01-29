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
        private readonly IApiConfigurationService _apiConfigService;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public TVService(
            IApiConfigurationService apiConfigService,
            IConfiguration config,
            HttpClient httpClient)
        {
            _apiConfigService = apiConfigService;
            _config = config;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<TVShowDetails> GetTVDetailsAsync(int tvShowId)
        {
            var data = await _httpClient.GetFromJsonAsync<TVShowDetails>($"tv/{tvShowId}?api_key={_config["API_KEY"]}");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            data.Poster_Path = ImageHelper.GetImageUrl(data.Poster_Path, PosterSizeType.Original, config);

            foreach (var season in data.Seasons)
            {
                season.Poster_Path = ImageHelper.GetImageUrl(season.Poster_Path, PosterSizeType.W500, config);
            }

            return data;
        }

        public async Task<IEnumerable<TVShow>> GetPopularTVAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/popular?api_key={_config["API_KEY"]}");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = ImageHelper.GetImageUrl(tvShow.Poster_Path, PosterSizeType.Original, config);
            }

            return data.Results;
        }

        public async Task<IEnumerable<TVShow>> GetTopRatedTVAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/top_rated?api_key={_config["API_KEY"]}");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = ImageHelper.GetImageUrl(tvShow.Poster_Path, PosterSizeType.Original, config);
            }

            return data.Results;
        }

        public async Task<IEnumerable<TVShow>> GetOnTheAirTVAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<TVResults>($"tv/on_the_air?api_key={_config["API_KEY"]}");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = ImageHelper.GetImageUrl(tvShow.Poster_Path, PosterSizeType.Original, config);
            }

            return data.Results;
        }

        public async Task<IEnumerable<Person>> GetTVCastAsync(int tvShowId)
        {
            var data = await _httpClient.GetFromJsonAsync<TVCredits>($"tv/{tvShowId}/credits?api_key={_config["API_KEY"]}");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            foreach (var person in data.Cast)
            {
                person.ImageUrl = ImageHelper.GetImageUrl(person.Profile_Path, PosterSizeType.W342, config);
            }

            return data.Cast;
        }

        public async Task<SeasonDetails> GetTVSeasonDetailsAsync(int tvShowId, int seasonNumber)
        {
            var data = await _httpClient.GetFromJsonAsync<SeasonDetails>($"tv/{tvShowId}/season/{seasonNumber}?api_key={_config["API_KEY"]}");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            data.Poster_Path = ImageHelper.GetImageUrl(data.Poster_Path, PosterSizeType.Original, config);

            foreach (var episode in data.Episodes)
            {
                episode.Still_Path = ImageHelper.GetImageUrl(episode.Still_Path, StillSizeType.W300, config);
            }

            return data;
        }
        #endregion
    }
}
