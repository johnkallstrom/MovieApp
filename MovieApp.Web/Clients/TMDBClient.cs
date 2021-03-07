using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Clients
{
    public class TMDBClient : ITMDBClient
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public TMDBClient(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<TValue> GetData<TValue>(string requestUri)
        {
            var data = await _httpClient.GetFromJsonAsync<TValue>(requestUri);

            return data;
        }
    }
}
