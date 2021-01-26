using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class SearchService : ISearchService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public SearchService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        #region Public Methods
        public async Task<SearchResults> GetMultiSearchAsync(MultiSearchParameters parameters)
        {
            var data = await _httpClient.GetFromJsonAsync<SearchResults>($"search/multi?api_key={_config["API_KEY"]}&query={parameters.Query}&page={parameters.Page}");

            var imageConfig = await GetImageConfiguration();

            foreach (var item in data.Results)
            {
                item.Poster_Path = ImageHelper.GetImageUrl(item.Poster_Path, FileSizeType.W342, imageConfig);
                item.Profile_Path = ImageHelper.GetImageUrl(item.Profile_Path, FileSizeType.W342, imageConfig);
            }

            return data;
        }
        #endregion

        #region Private Methods
        private async Task<Image> GetImageConfiguration()
        {
            var configuration = await _httpClient.GetFromJsonAsync<Configuration>($"configuration?api_key={_config["API_KEY"]}");

            return configuration.Images;
        }
        #endregion
    }
}
