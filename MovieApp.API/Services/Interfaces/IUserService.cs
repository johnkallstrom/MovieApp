using MovieApp.API.Entities;
using MovieApp.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public interface IUserService
    {
        public Task<LoginResponse> LoginUserAsync(LoginRequest request);
        public Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(int userId);
    }
}
