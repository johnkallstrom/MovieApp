using MovieApp.Domain.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IUserHttpService
    {
        Task<UpdateUserResponse> UpdateUserAsync(int userId, UpdateUserDto model);
        Task<UserDto> GetUserAsync(int userId);
        Task<bool> DeleteUserAsync(int userId);
    }
}
