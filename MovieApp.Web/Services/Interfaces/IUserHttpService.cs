using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IUserHttpService
    {
        Task<IEnumerable<MovieListDto>> GetUserMovieListsAsync(int userId);
        Task<UpdateUserResponse> UpdateUserAsync(int userId, UpdateUserRequest model);
        Task<UserDto> GetUserAsync(int userId);
        Task<bool> DeleteUserAsync(int userId);
    }
}
