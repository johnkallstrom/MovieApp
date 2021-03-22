using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IListHttpService
    {
        Task<DeleteMovieItemResponse> DeleteMovieFromListAsync(int movieListId, DeleteMovieItemRequest request);
        Task<AddMovieItemResponse> AddMovieToListAsync(int movieListId, AddMovieItemRequest request);
        Task<bool> DeleteMovieListAsync(int movieListId);
        Task<UpdateMovieListResponse> UpdateMovieListAsync(int movieListId, UpdateMovieListRequest request);
        Task<MovieListDto> GetMovieListAsync(int movieListId);
        Task<CreateMovieListResponse> CreateMovieListAsync(int userId, CreateMovieListRequest request);
    }
}
