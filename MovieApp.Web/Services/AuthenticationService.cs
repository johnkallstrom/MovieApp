using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using MovieApp.Domain.Models;
using MovieApp.Web.State;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public AuthenticationService(
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
                ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResponse.Id, loginResponse.FirstName, loginResponse.LastName, loginResponse.Email);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponse.Token);
            }

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
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
