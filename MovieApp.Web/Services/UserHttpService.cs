using MovieApp.Domain.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class UserHttpService : IUserHttpService
    {
        private readonly HttpClient _httpClient;

        public UserHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDto>($"users/{userId}");

            return user;
        }
    }
}
