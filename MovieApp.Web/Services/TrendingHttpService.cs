using Microsoft.Extensions.Configuration;
using MovieApp.Web.Clients;
using MovieApp.Web.Enums;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class TrendingHttpService : ITrendingHttpService
    {
        private const string API_KEY = "TMDB:ApiKey";
        private const string IMAGE_URL = "TMDB:ImageBaseUrl";
        private const string PLACEHOLDER_IMAGE_URL = "TMDB:PlaceholderImageBaseUrl";

        private readonly IConfiguration _config;
        private readonly ITMDBClient _tmdbClient;

        public TrendingHttpService(
            IConfiguration config,
            ITMDBClient tmdbClient)
        {
            _config = config;
            _tmdbClient = tmdbClient;
        }

        public async Task<IEnumerable<Media>> GetTrendingItemsAsync(string mediaType, string timeWindowType)
        {
            var data = await _tmdbClient.GetData<MediaResults>($"trending/{mediaType}/{timeWindowType}?api_key={_config[API_KEY]}");

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var media in data.Results)
            {
                media.Url = GetMediaUrl(media.Media_Type, media.Id);
                media.Backdrop_Path = !string.IsNullOrEmpty(media.Backdrop_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], BackdropSizeType.W1280, media.Backdrop_Path)) : placeholderUrl;
                media.Poster_Path = !string.IsNullOrEmpty(media.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, media.Poster_Path)) : placeholderUrl;
                media.Profile_Path = !string.IsNullOrEmpty(media.Profile_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], ProfileSizeType.H632, media.Profile_Path)) : placeholderUrl;
            }

            return data.Results;
        }

        private string GetMediaUrl(string type, int id)
        {
            string url = string.Empty;

            switch (type)
            {
                case MediaType.Movie:
                    url = $"/movie/{id}";
                    break;
                case MediaType.TV:
                    url = $"/tv-shows/{id}";
                    break;
                case MediaType.Person:
                    url = $"/person/{id}";
                    break;
            }

            return url;
        }
    }
}
