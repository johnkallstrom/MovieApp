using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IAuthenticationService
    {
        Task LogoutUser();
        Task<LoginResponse> LoginUser(LoginRequest request);
        Task<RegisterResponse> RegisterUser(RegisterRequest request);
    }
}
