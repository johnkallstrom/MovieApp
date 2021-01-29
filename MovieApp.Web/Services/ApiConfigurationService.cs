using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using MovieApp.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class ApiConfigurationService : IApiConfigurationService
    {
        private const string CONFIG_KEY = "API_CONFIG";

        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public ApiConfigurationService(
            ILocalStorageService localStorage,
            IConfiguration config,
            HttpClient httpClient)
        {
            _localStorage = localStorage;
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<ApiConfiguration> GetApiConfigurationAsync()
        {
            var storedConfig = await _localStorage.GetItemAsync<ApiConfiguration>(CONFIG_KEY);

            if (storedConfig == null)
            {
                var config = await _httpClient.GetFromJsonAsync<ApiConfiguration>($"configuration?api_key={_config["API_KEY"]}");

                if (config != null)
                    await _localStorage.SetItemAsync(CONFIG_KEY, config);

                return config;
            }

            return storedConfig;
        }
    }
}
