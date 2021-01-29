using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class MovieService : IMovieService
    {
        private const string API_KEY = "API_KEY";
        private const string IMAGE_BASE_URL = "IMAGE_BASE_URL";

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public MovieService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<IEnumerable<Movie>> GetSimilarMoviesAsync(int movieId)
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/{movieId}/similar?api_key={_config[API_KEY]}");

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.W500, movie.Poster_Path);
            }

            return data.Results;
        }

        public async Task<IEnumerable<Person>> GetMovieCastAsync(int movieId)
        {
            var data = await _httpClient.GetFromJsonAsync<MovieCredits>($"movie/{movieId}/credits?api_key={_config[API_KEY]}");

            foreach (var person in data.Cast)
            {
                person.Profile_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.W342, person.Profile_Path);
            }

            return data.Cast;
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int movieId)
        {
            var data = await _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{movieId}?api_key={_config[API_KEY]}");

            data.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.Original, data.Poster_Path);

            return data;
        }

        public async Task<IEnumerable<Movie>> GetPopularMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/popular?api_key={_config[API_KEY]}");

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.Original, movie.Poster_Path);
            }

            return data.Results;
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/top_rated?api_key={_config[API_KEY]}");

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.Original, movie.Poster_Path);
            }

            return data.Results;
        }

        public async Task<IEnumerable<Movie>> GetUpcomingMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/upcoming?api_key={_config[API_KEY]}");

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = ImageHelper.GetImageUrl(_config[IMAGE_BASE_URL], PosterSizeType.Original, movie.Poster_Path);
            }

            return data.Results;
        }
        #endregion
    }
}
