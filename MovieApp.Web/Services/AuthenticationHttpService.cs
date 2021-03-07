using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using MovieApp.Domain.Models;
using MovieApp.Web.State;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class AuthenticationHttpService : IAuthenticationHttpService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public AuthenticationHttpService(
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage,
            IConfiguration config,
            HttpClient httpClient)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> LoginUser(LoginRequest request)
        {
            HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("users/login", request);

            var loginResponse = await httpResponse.Content.ReadFromJsonAsync<LoginResponse>();

            if (loginResponse.Success)
            {
                await _localStorage.SetItemAsync(_config["JWT:LocalStorageKey"], loginResponse.Token);
                ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResponse.Id, loginResponse.Email);
            }

            var baseUri = _httpClient.BaseAddress.AbsoluteUri;

            System.Console.WriteLine("AuthService HttpClient Base Uri: " + baseUri);

            return loginResponse;
        }

        public async Task<RegisterResponse> RegisterUser(RegisterRequest request)
        {
            HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("users/register", request);

            var registerResponse = await httpResponse.Content.ReadFromJsonAsync<RegisterResponse>();

            return registerResponse;
        }

        public async Task LogoutUser()
        {
            await _localStorage.RemoveItemAsync(_config["JWT:LocalStorageKey"]);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAnonymous();
        }
    }
}
