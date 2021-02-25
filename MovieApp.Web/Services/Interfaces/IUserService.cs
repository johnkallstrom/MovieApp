using MovieApp.Domain.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IUserService
    {
        Task<LoginResponse> LoginUser(LoginRequest request);
        Task<RegisterResponse> RegisterUser(RegisterRequest request);
    }
}
