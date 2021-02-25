using Microsoft.Extensions.Configuration;
using MovieApp.Domain.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public UserService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> LoginUser(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("users/login", request);

            var result = new LoginResponse();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (!string.IsNullOrEmpty(result.Token))
                {
                    // To do: store token in localstorage or session
                }
            }

            return result;
        }

        public async Task<RegisterResponse> RegisterUser(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("users/register", request);

            var result = new RegisterResponse();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
            }

            return result;
        }
    }
}
