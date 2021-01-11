using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class MovieService : IMovieService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationService _configService;
        private readonly HttpClient _httpClient;

        public MovieService(
            IConfiguration config,
            IConfigurationService configService,
            HttpClient httpClient)
        {
            _config = config;
            _configService = configService;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<IEnumerable<Movie>> GetMoviesBySearchAsync(string query)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"search/movie?api_key={_config["API_KEY"]}&query={query}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var data = JsonSerializer.Deserialize<MovieResults>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var movies = data.Results;

            var imageConfig = await GetImageConfiguration();

            foreach (var movie in movies)
            {
                movie.ImageUrl = GetImageUrl(movie.Poster_Path, PosterSizeType.W500, imageConfig);
            }

            return movies;
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int movieId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"movie/{movieId}?api_key={_config["API_KEY"]}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var movie = JsonSerializer.Deserialize<MovieDetails>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var imageConfig = await GetImageConfiguration();

            movie.ImageUrl = GetImageUrl(movie.Poster_Path, PosterSizeType.W780, imageConfig);

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetPopularMoviesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"movie/popular?api_key={_config["API_KEY"]}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var data = JsonSerializer.Deserialize<MovieResults>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var movies = data.Results;

            var imageConfig = await GetImageConfiguration();

            foreach (var movie in movies)
            {
                movie.ImageUrl = GetImageUrl(movie.Poster_Path, PosterSizeType.W342, imageConfig);
            }

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMoviesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"movie/top_rated?api_key={_config["API_KEY"]}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var data = JsonSerializer.Deserialize<MovieResults>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var movies = data.Results;

            var imageConfig = await GetImageConfiguration();

            foreach (var movie in movies)
            {
                movie.ImageUrl = GetImageUrl(movie.Poster_Path, PosterSizeType.W342, imageConfig);
            }

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetUpcomingMoviesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"movie/upcoming?api_key={_config["API_KEY"]}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var data = JsonSerializer.Deserialize<MovieResults>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var movies = data.Results;

            var imageConfig = await GetImageConfiguration();

            foreach (var movie in movies)
            {
                movie.ImageUrl = GetImageUrl(movie.Poster_Path, PosterSizeType.W342, imageConfig);
            }

            return movies;
        }
        #endregion

        #region Private Methods
        private async Task<Image> GetImageConfiguration()
        {
            var configuration = await _configService.GetApiConfigurationAsync();

            return configuration.Images;
        }

        private string GetImageUrl(string filePath, string sizeType, Image imageConfig)
        {
            string url = string.Empty;

            if (!string.IsNullOrEmpty(filePath))
            {
                url = $"{imageConfig.Secure_Base_Url}{imageConfig.Poster_Sizes.FirstOrDefault(s => s.StartsWith(sizeType))}/{filePath}";
            }

            return url;
        }
        #endregion
    }
}
