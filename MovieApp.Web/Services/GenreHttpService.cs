using Microsoft.Extensions.Configuration;
using MovieApp.Web.Clients;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class GenreHttpService : IGenreHttpService
    {
        private const string API_KEY = "TMDB:ApiKey";

        private readonly IConfiguration _config;
        private readonly ITMDBClient _tmdbClient;

        public GenreHttpService(
            IConfiguration config,
            ITMDBClient tmdbClient)
        {
            _config = config;
            _tmdbClient = tmdbClient;
        }

        public async Task<IEnumerable<Genre>> GetMovieGenresAsync()
        {
            var data = await _tmdbClient.GetData<GenreResults>($"genre/movie/list?api_key={_config[API_KEY]}");

            return data.Genres;
        }

        public async Task<IEnumerable<Genre>> GetTVGenresAsync()
        {
            var data = await _tmdbClient.GetData<GenreResults>($"genre/tv/list?api_key={_config[API_KEY]}");

            return data.Genres;
        }
    }
}
