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

        public async Task<UpdateUserResponse> UpdateUserAsync(int userId, UpdateUserDto model)
        {
            HttpResponseMessage httpResponse = await _httpClient.PutAsJsonAsync($"users/{userId}", model);

            var response = await httpResponse.Content.ReadFromJsonAsync<UpdateUserResponse>();

            return response;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"users/{userId}");

            bool succeeded = false;

            if (response.IsSuccessStatusCode)
            {
                succeeded = true;
            }

            return succeeded;
        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDto>($"users/{userId}");

            return user;
        }
    }
}
