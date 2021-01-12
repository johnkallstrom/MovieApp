using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationService _configService;
        private readonly HttpClient _httpClient;

        public PeopleService(
            IConfigurationService configService,
            IConfiguration config,
            HttpClient httpClient)
        {
            _configService = configService;
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<PersonDetails> GetPersonAsync(int personId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"person/{personId}?api_key={_config["API_KEY"]}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var person = JsonSerializer.Deserialize<PersonDetails>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var config = await _configService.GetApiConfigurationAsync();

            person.ImageUrl = ImageHelper.GetImageUrl(person.Profile_Path, PosterSizeType.W780, config.Images);

            return person;
        }

        public async Task<IEnumerable<Movie>> GetPersonMoviesAsync(int personId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"person/{personId}/movie_credits?api_key={_config["API_KEY"]}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var data = JsonSerializer.Deserialize<PersonCredits>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var config = await _configService.GetApiConfigurationAsync();

            foreach (var movie in data.Cast)
            {
                movie.ImageUrl = ImageHelper.GetImageUrl(movie.Poster_Path, PosterSizeType.W342, config.Images);
            }

            return data.Cast;
        }
    }
}
