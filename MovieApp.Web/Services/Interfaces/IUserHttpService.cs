using MovieApp.Domain.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IUserHttpService
    {
        Task<bool> DeleteUserAsync(int userId);
        Task<UserDto> GetUserAsync(int userId);
    }
}
