using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IMovieListHttpService
    {
        Task<IEnumerable<MovieListDto>> GetMovieListsAsync(int userId);
        Task<CreateMovieListResponse> CreateMovieListAsync(int userId, CreateMovieListRequest request);
    }
}
