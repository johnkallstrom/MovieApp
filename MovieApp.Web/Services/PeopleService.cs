using Microsoft.Extensions.Configuration;
using MovieApp.Web.Enums;
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
        private const string API_KEY = "ApiKey";
        private const string IMAGE_URL = "ImageBaseUrl";

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public PeopleService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<IEnumerable<Person>> GetPopularPeopleAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<PeopleResults>($"person/popular?api_key={_config[API_KEY]}");

            foreach (var person in data.Results)
            {
                person.Profile_Path = ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.Original, person.Profile_Path));
            }

            return data.Results;
        }

        public async Task<PersonDetails> GetPersonAsync(int personId)
        {
            var data = await _httpClient.GetFromJsonAsync<PersonDetails>($"person/{personId}?api_key={_config[API_KEY]}");

            data.Profile_Path = ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.Original, data.Profile_Path));

            return data;
        }

        public async Task<IEnumerable<Movie>> GetPersonMoviesAsync(int personId)
        {
            var data = await _httpClient.GetFromJsonAsync<PersonCredits>($"person/{personId}/movie_credits?api_key={_config[API_KEY]}");

            foreach (var movie in data.Cast)
            {
                movie.Poster_Path = ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.Original, movie.Poster_Path));
            }

            return data.Cast;
        }
        #endregion
    }
}
