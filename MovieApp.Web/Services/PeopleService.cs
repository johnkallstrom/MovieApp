using Microsoft.Extensions.Configuration;
using MovieApp.Web.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public PeopleService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<PersonDetails> GetPersonAsync(int personId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"person/{personId}?api_key={_config["API_KEY"]}");

            string content = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var person = JsonSerializer.Deserialize<PersonDetails>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // todo: fix image url

            return person;
        }
    }
}
