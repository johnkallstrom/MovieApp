using MovieApp.Domain.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class FavoriteHttpService : IFavoriteHttpService
    {
        private readonly HttpClient _httpClient;

        public FavoriteHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddMovieToFavorites(int userId, AddFavoriteMovieDto model)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"favorites/movie/{userId}", model);

            bool succeeded = false;
            if (response.IsSuccessStatusCode)
            {
                succeeded = true;
            }

            return succeeded;
        }

        public async Task<bool> MovieExistsAsFavorite(int userId, int tmdbId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"favorites/movie/{userId}/{tmdbId}");

            bool exists = false;

            if (response.IsSuccessStatusCode)
            {
                exists = true;
            }

            return exists;
        }

        public async Task DeleteMovieFromFavorites(int userId, int tmdbId)
        {
            await _httpClient.DeleteAsync($"favorites/movie/{userId}/{tmdbId}");
        }
    }
}
