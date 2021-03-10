using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public interface IUserService
    {
        public Task<bool> UserExistsAsync(int userId);
        public Task<LoginResponse> LoginAsync(LoginRequest request);
        public Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetAsync(int userId);
    }
}
