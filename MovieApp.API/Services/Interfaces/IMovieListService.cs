using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public interface IMovieListService
    {
        Task<DeleteMovieItemResponse> DeleteMovieItemAsync(int movieListId, DeleteMovieItemRequest request);
        Task<AddMovieItemResponse> AddMovieItemAsync(int movieListId, AddMovieItemRequest request);
        UpdateMovieListResponse UpdateMovieList(MovieList movieList);
        void DeleteMovieList(MovieList movieList);
        Task<MovieList> GetMovieListAsync(int movieListId);
        Task<IEnumerable<MovieList>> GetMovieListsAsync(int userId);
        Task<CreateMovieListResponse> CreateMovieListAsync(int userId, CreateMovieListRequest request);
    }
}
