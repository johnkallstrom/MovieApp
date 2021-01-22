using Microsoft.Extensions.Configuration;
using MovieApp.Web.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public ConfigurationService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<Configuration> GetApiConfigurationAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"configuration?api_key={_config["API_KEY"]}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var data = JsonSerializer.Deserialize<Configuration>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return data;
        }

        public Configuration GetApiConfiguration()
        {
            HttpResponseMessage response = _httpClient.GetAsync($"configuration?api_key={_config["API_KEY"]}").Result;

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = response.Content.ReadAsStringAsync().Result;
            }

            var data = JsonSerializer.Deserialize<Configuration>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return data;
        }
    }
}
