using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class MovieService : IMovieService
    {
        private const string API_KEY = "ad36218fe0adbcd7be01bb885894b1e1";
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMoviesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"movie/top_rated?api_key={API_KEY}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            MovieResults results = JsonSerializer.Deserialize<MovieResults>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return results.Results;
        }
    }
}
