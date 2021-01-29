using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IApiConfigurationService _apiConfigService;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public PeopleService(
            IApiConfigurationService apiConfigService,
            IConfiguration config,
            HttpClient httpClient)
        {
            _apiConfigService = apiConfigService;
            _config = config;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<IEnumerable<Person>> GetPopularPeopleAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<PeopleResults>($"person/popular?api_key={_config["API_KEY"]}");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            foreach (var person in data.Results)
            {
                person.ImageUrl = ImageHelper.GetImageUrl(person.Profile_Path, ProfileSizeType.Original, config);
            }

            return data.Results;
        }

        public async Task<PersonDetails> GetPersonAsync(int personId)
        {
            var data = await _httpClient.GetFromJsonAsync<PersonDetails>($"person/{personId}?api_key={_config["API_KEY"]}");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            data.ImageUrl = ImageHelper.GetImageUrl(data.Profile_Path, ProfileSizeType.Original, config);

            return data;
        }

        public async Task<IEnumerable<Movie>> GetPersonMoviesAsync(int personId)
        {
            var data = await _httpClient.GetFromJsonAsync<PersonCredits>($"person/{personId}/movie_credits?api_key={_config["API_KEY"]}");

            var config = await _apiConfigService.GetApiConfigurationAsync();

            foreach (var movie in data.Cast)
            {
                movie.ImageUrl = ImageHelper.GetImageUrl(movie.Poster_Path, PosterSizeType.W342, config);
            }

            return data.Cast;
        }
        #endregion
    }
}
