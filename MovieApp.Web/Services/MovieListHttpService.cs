using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class MovieListHttpService : IMovieListHttpService
    {
        private readonly HttpClient _httpClient;

        public MovieListHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CreateMovieListResponse> CreateMovieListAsync(int userId, CreateMovieListRequest request)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync($"lists/{userId}/create", request);

            var response = await httpResponse.Content.ReadFromJsonAsync<CreateMovieListResponse>();

            return response;
        }

        public async Task<MovieListDto> GetMovieListAsync(int userId, int movieListId)
        {
            var movieList = await _httpClient.GetFromJsonAsync<MovieListDto>($"lists/{userId}/{movieListId}");

            return movieList;
        }

        public async Task<IEnumerable<MovieListDto>> GetMovieListsAsync(int userId)
        {
            var movieLists = await _httpClient.GetFromJsonAsync<IEnumerable<MovieListDto>>($"lists/{userId}");

            return movieLists;
        }
    }
}
