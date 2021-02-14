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
        private const string PLACEHOLDER_IMAGE_URL = "PlaceholderImageBaseUrl";

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
        public async Task<PeopleResults> GetPopularPeopleAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<PeopleResults>($"person/popular?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var person in data.Results)
            {
                person.Profile_Path = !string.IsNullOrEmpty(person.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, person.Profile_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<PeopleResults> GetPopularPeopleAsync(int page)
        {
            var data = await _httpClient.GetFromJsonAsync<PeopleResults>($"person/popular?api_key={_config[API_KEY]}&page={page}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var person in data.Results)
            {
                person.Profile_Path = !string.IsNullOrEmpty(person.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, person.Profile_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<PersonDetails> GetPersonAsync(int personId)
        {
            var data = await _httpClient.GetFromJsonAsync<PersonDetails>($"person/{personId}?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            data.Profile_Path = !string.IsNullOrEmpty(data.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, data.Profile_Path)) : placeholderUrl;

            return data;
        }

        public async Task<IEnumerable<Movie>> GetPersonMoviesAsync(int personId)
        {
            var data = await _httpClient.GetFromJsonAsync<PersonCredits>($"person/{personId}/movie_credits?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var movie in data.Cast)
            {
                movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, movie.Poster_Path)) : placeholderUrl;
            }

            return data.Cast;
        }
        #endregion
    }
}
