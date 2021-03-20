using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IListHttpService
    {
        Task<bool> DeleteMovieFromListAsync(int movieListItemId);
        Task<AddMovieResponse> AddMovieToListAsync(int movieListId, AddMovieRequest request);
        Task<bool> DeleteMovieListAsync(int movieListId);
        Task<UpdateMovieListResponse> UpdateMovieListAsync(int movieListId, UpdateMovieListRequest request);
        Task<MovieListDetailsDto> GetMovieListAsync(int movieListId);
        Task<CreateMovieListResponse> CreateMovieListAsync(int userId, CreateMovieListRequest request);
    }
}
