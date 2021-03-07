using MovieApp.Domain.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IUserHttpService
    {
        Task<UserDto> GetUserAsync(int userId);
    }
}
