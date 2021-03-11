using MovieApp.Domain.Entities;
using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public interface IUserService
    {
        public void UpdateUser(User user);
        public void DeleteUser(User user);
        public Task<LoginResponse> LoginAsync(LoginRequest request);
        public Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        public Task<User> GetUserAsync(int userId);
        public Task<IEnumerable<User>> GetUsersAsync();
    }
}
