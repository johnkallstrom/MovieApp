using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Web.State
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(
            HttpClient httpClient,
            IConfiguration config,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _config = config;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var storedToken = await _localStorage.GetItemAsync<string>(_config["JWT:LocalStorageKey"]);

            if (string.IsNullOrEmpty(storedToken))
            {
                var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
                return await Task.FromResult(new AuthenticationState(anonymousUser));
            }

            var claims = ParseClaimsFromJwtToken(storedToken);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, _config["AuthenticationType"]));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", storedToken);

            return await Task.FromResult(new AuthenticationState(authenticatedUser));
        }

        public void MarkUserAsAuthenticated(int id, string username, string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email)
            };

            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, _config["AuthenticationType"]));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsAnonymous()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwtToken(string token)
        {
            var claims = new List<Claim>();

            var handler = new JwtSecurityTokenHandler();

            if (!string.IsNullOrWhiteSpace(token))
            {
                var jwtToken = handler.ReadJwtToken(token);

                foreach (var claim in jwtToken.Claims)
                {
                    claims.Add(claim);
                }
            }

            return claims;
        }
    }
}
