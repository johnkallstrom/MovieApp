using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public interface IMovieListService
    {
        void DeleteMovieList(MovieList movieList);
        Task<MovieList> GetMovieListAsync(int userId, int movieListId);
        Task<IEnumerable<MovieList>> GetMovieListsAsync(int userId);
        Task<CreateMovieListResponse> CreateMovieListAsync(int userId, CreateMovieListRequest request);
    }
}
