﻿using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class ListHttpService : IListHttpService
    {
        private readonly HttpClient _httpClient;

        public ListHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DeleteMovieItemResponse> DeleteMovieFromListAsync(int movieListId, DeleteMovieItemRequest request)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync($"lists/{movieListId}/remove", request);

            var response = await httpResponse.Content.ReadFromJsonAsync<DeleteMovieItemResponse>();

            return response;
        }

        public async Task<AddMovieItemResponse> AddMovieToListAsync(int movieListId, AddMovieItemRequest request)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync($"lists/{movieListId}/add", request);

            var response = await httpResponse.Content.ReadFromJsonAsync<AddMovieItemResponse>();

            return response;
        }

        public async Task<UpdateMovieListResponse> UpdateMovieListAsync(int movieListId, UpdateMovieListRequest request)
        {
            var httpResponse = await _httpClient.PutAsJsonAsync($"lists/{movieListId}", request);

            var response = await httpResponse.Content.ReadFromJsonAsync<UpdateMovieListResponse>();

            return response;
        }

        public async Task<CreateMovieListResponse> CreateMovieListAsync(int userId, CreateMovieListRequest request)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync($"lists/{userId}", request);

            var response = await httpResponse.Content.ReadFromJsonAsync<CreateMovieListResponse>();

            return response;
        }

        public async Task<MovieListDto> GetMovieListAsync(int movieListId)
        {
            var movieList = await _httpClient.GetFromJsonAsync<MovieListDto>($"lists/{movieListId}");

            return movieList;
        }

        public async Task<bool> DeleteMovieListAsync(int movieListId)
        {
            var httpResponse = await _httpClient.DeleteAsync($"lists/{movieListId}");

            bool succeeded = false;

            if (httpResponse.IsSuccessStatusCode)
            {
                succeeded = true;
            }

            return succeeded;
        }
    }
}
