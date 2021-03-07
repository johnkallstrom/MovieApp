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
    public class MovieHttpService : IMovieHttpService
    {
        private const string API_KEY = "TMDB:ApiKey";
        private const string IMAGE_URL = "TMDB:ImageBaseUrl";
        private const string PLACEHOLDER_IMAGE_URL = "TMDB:PlaceholderImageBaseUrl";

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public MovieHttpService(
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

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, movie.Poster_Path)) : placeholderUrl;
            }

            return data.Results;
        }

        public async Task<IEnumerable<Person>> GetMovieCastAsync(int movieId)
        {
            var data = await _httpClient.GetFromJsonAsync<MovieCredits>($"movie/{movieId}/credits?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var person in data.Cast)
            {
                person.Profile_Path = !string.IsNullOrEmpty(person.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, person.Profile_Path)) : placeholderUrl;
            }

            return data.Cast;
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int movieId)
        {
            var movie = await _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{movieId}?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 1000, 1500));

            movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.Original, movie.Poster_Path)) : placeholderUrl;

            return movie;
        }

        public async Task<MovieResults> GetPopularMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/popular?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, movie.Poster_Path)) : placeholderUrl;
            }

            var baseUri = _httpClient.BaseAddress.AbsoluteUri;
            System.Console.WriteLine("MovieService HttpClient BaseUri: " + baseUri);

            return data;
        }


        public async Task<MovieResults> GetPopularMoviesAsync(int page)
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/popular?api_key={_config[API_KEY]}&page={page}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, movie.Poster_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<MovieResults> GetTopRatedMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/top_rated?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, movie.Poster_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<MovieResults> GetTopRatedMoviesAsync(int page)
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/top_rated?api_key={_config[API_KEY]}&page={page}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, movie.Poster_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<MovieResults> GetUpcomingMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/upcoming?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, movie.Poster_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<MovieResults> GetUpcomingMoviesAsync(int page)
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/upcoming?api_key={_config[API_KEY]}&page={page}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, movie.Poster_Path)) : placeholderUrl;
            }

            return data;
        }
        #endregion
    }
}
