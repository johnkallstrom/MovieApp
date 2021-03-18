using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IListHttpService
    {
        Task<bool> DeleteMovieListAsync(int movieListId);
        Task<UpdateMovieListResponse> UpdateMovieListAsync(int movieListId, UpdateMovieListRequest request);
        Task<MovieListDto> GetMovieListAsync(int movieListId);
        Task<CreateMovieListResponse> CreateMovieListAsync(int userId, CreateMovieListRequest request);
    }
}
