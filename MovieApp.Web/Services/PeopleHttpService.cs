using Microsoft.Extensions.Configuration;
using MovieApp.Web.Clients;
using MovieApp.Web.Enums;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class PeopleHttpService : IPeopleHttpService
    {
        private const string API_KEY = "TMDB:ApiKey";
        private const string IMAGE_URL = "TMDB:ImageBaseUrl";
        private const string PLACEHOLDER_IMAGE_URL = "TMDB:PlaceholderImageBaseUrl";

        private readonly IConfiguration _config;
        private readonly ITMDBClient _tmdbClient;

        public PeopleHttpService(
            IConfiguration config,
            ITMDBClient tmdbClient)
        {
            _config = config;
            _tmdbClient = tmdbClient;
        }

        #region Public Methods
        public async Task<PeopleResults> GetPeopleBySearchAsync(string query)
        {
            var data = await _tmdbClient.GetData<PeopleResults>($"search/person?api_key={_config[API_KEY]}&query={query}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var person in data.Results)
            {
                person.Profile_Path = !string.IsNullOrEmpty(person.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, person.Profile_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<PersonDetails> GetPersonAsync(int personId)
        {
            var data = await _tmdbClient.GetData<PersonDetails>($"person/{personId}?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            data.Profile_Path = !string.IsNullOrEmpty(data.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, data.Profile_Path)) : placeholderUrl;

            return data;
        }

        public async Task<IEnumerable<Movie>> GetPersonMoviesAsync(int personId)
        {
            var data = await _tmdbClient.GetData<PersonCredits>($"person/{personId}/movie_credits?api_key={_config[API_KEY]}");

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
