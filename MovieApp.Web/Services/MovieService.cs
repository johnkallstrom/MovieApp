using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class MovieService : IMovieService
    {
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
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/{movieId}/similar?api_key={_config["API_KEY"]}");

            var imageConfig = await GetImageConfiguration();

            foreach (var movie in data.Results)
            {
                movie.ImageUrl = ImageHelper.GetImageUrl(movie.Poster_Path, FileSizeType.W500, imageConfig);
            }

            return data.Results;
        }

        public async Task<IEnumerable<Person>> GetMovieCastAsync(int movieId)
        {
            var data = await _httpClient.GetFromJsonAsync<MovieCredits>($"movie/{movieId}/credits?api_key={_config["API_KEY"]}");

            var imageConfig = await GetImageConfiguration();

            foreach (var person in data.Cast)
            {
                person.ImageUrl = ImageHelper.GetImageUrl(person.Profile_Path, FileSizeType.W342, imageConfig);
            }

            return data.Cast;
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int movieId)
        {
            var data = await _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{movieId}?api_key={_config["API_KEY"]}");

            var imageConfig = await GetImageConfiguration();

            data.ImageUrl = ImageHelper.GetImageUrl(data.Poster_Path, FileSizeType.Original, imageConfig);

            return data;
        }

        public async Task<IEnumerable<Movie>> GetPopularMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/popular?api_key={_config["API_KEY"]}");

            var imageConfig = await GetImageConfiguration();

            foreach (var movie in data.Results)
            {
                movie.ImageUrl = ImageHelper.GetImageUrl(movie.Poster_Path, FileSizeType.Original, imageConfig);
            }

            return data.Results;
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/top_rated?api_key={_config["API_KEY"]}");

            var imageConfig = await GetImageConfiguration();

            foreach (var movie in data.Results)
            {
                movie.ImageUrl = ImageHelper.GetImageUrl(movie.Poster_Path, FileSizeType.Original, imageConfig);
            }

            return data.Results;
        }

        public async Task<IEnumerable<Movie>> GetUpcomingMoviesAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<MovieResults>($"movie/upcoming?api_key={_config["API_KEY"]}");

            var imageConfig = await GetImageConfiguration();

            foreach (var movie in data.Results)
            {
                movie.ImageUrl = ImageHelper.GetImageUrl(movie.Poster_Path, FileSizeType.Original, imageConfig);
            }

            return data.Results;
        }
        #endregion

        #region Private Methods
        private async Task<Image> GetImageConfiguration()
        {
            var configuration = await _httpClient.GetFromJsonAsync<Configuration>($"configuration?api_key={_config["API_KEY"]}");

            return configuration.Images;
        }
        #endregion
    }
}
