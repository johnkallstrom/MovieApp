using Microsoft.Extensions.Configuration;
using MovieApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class GenreHttpService : IGenreHttpService
    {
        private const string API_KEY = "TMDB:ApiKey";

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public GenreHttpService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Genre>> GetMovieGenresAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<GenreResults>($"genre/movie/list?api_key={_config[API_KEY]}");

            return data.Genres;
        }

        public async Task<IEnumerable<Genre>> GetTVGenresAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<GenreResults>($"genre/tv/list?api_key={_config[API_KEY]}");

            return data.Genres;
        }
    }
}
