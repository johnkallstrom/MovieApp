using Microsoft.Extensions.Configuration;
using MovieApp.Web.Enums;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public class DiscoverHttpService : IDiscoverHttpService
    {
        private const string API_KEY = "TMDB:ApiKey";
        private const string IMAGE_URL = "TMDB:ImageBaseUrl";
        private const string PLACEHOLDER_IMAGE_URL = "TMDB:PlaceholderImageBaseUrl";

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public DiscoverHttpService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<MovieResults> GetMoviesAsync(MovieParameters parameters)
        {
            string url = GetMovieApiUrl(parameters);

            var data = await _httpClient.GetFromJsonAsync<MovieResults>(url);

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var movie in data.Results)
            {
                movie.Poster_Path = !string.IsNullOrEmpty(movie.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, movie.Poster_Path)) : placeholderUrl;
            }

            return data;
        }

        public async Task<TVResults> GetTVAsync(TVParameters parameters)
        {
            string url = GetTVApiUrl(parameters);

            var data = await _httpClient.GetFromJsonAsync<TVResults>(url);

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = !string.IsNullOrEmpty(tvShow.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, tvShow.Poster_Path)) : placeholderUrl;
            }

            return data;
        }

        private string GetMovieApiUrl(MovieParameters parameters)
        {
            var builder = new StringBuilder($"discover/movie?api_key={_config[API_KEY]}&page={parameters.Page}");

            if (!string.IsNullOrEmpty(parameters.SortOrder))
            {
                builder.AppendLine($"&sort_by={parameters.SortOrder}");
            }

            if (parameters.ReleaseYear is not 0)
            {
                builder.AppendLine($"&primary_release_year={parameters.ReleaseYear}");
            }

            if (!string.IsNullOrEmpty(parameters.FromReleaseDate))
            {
                builder.AppendLine($"&primary_release_date.gte={parameters.FromReleaseDate}");
            }

            if (!string.IsNullOrEmpty(parameters.ToReleaseDate))
            {
                builder.AppendLine($"&primary_release_date.lte={parameters.ToReleaseDate}");
            }

            if (parameters.GenreIds.Count() is not 0)
            {
                builder.AppendLine("&with_genres=");

                foreach (var genreId in parameters.GenreIds)
                {
                    if (genreId == parameters.GenreIds.Last())
                    {
                        builder.AppendLine($"{genreId}");
                    }
                    else
                    {
                        builder.AppendLine($"{genreId},");
                    }
                }
            }

            if (parameters.ActorIds.Count() is not 0)
            {
                builder.AppendLine("&with_cast=");

                foreach (var actorId in parameters.ActorIds)
                {
                    if (actorId == parameters.ActorIds.Last())
                    {
                        builder.AppendLine($"{actorId}");
                    }
                    else
                    {
                        builder.AppendLine($"{actorId},");
                    }
                }
            }

            if (parameters.DirectorIds.Count() is not 0)
            {
                builder.AppendLine("&with_crew=");

                foreach (var directorId in parameters.DirectorIds)
                {
                    if (directorId == parameters.DirectorIds.Last())
                    {
                        builder.AppendLine($"{directorId}");
                    }
                    else
                    {
                        builder.AppendLine($"{directorId},");
                    }
                }
            }

            if (parameters.KeywordIds.Count() is not 0)
            {
                builder.AppendLine("&with_keywords=");

                foreach (var keywordId in parameters.KeywordIds)
                {
                    if (keywordId == parameters.KeywordIds.Last())
                    {
                        builder.AppendLine($"{keywordId}");
                    }
                    else
                    {
                        builder.AppendLine($"{keywordId},");
                    }
                }
            }

            if (parameters.Runtime is not 0)
            {
                builder.AppendLine($"&with_runtime.gte={parameters.Runtime}");
            }

            builder.AppendLine("&include_adult=false");

            return builder.ToString();
        }

        private string GetTVApiUrl(TVParameters parameters)
        {
            var builder = new StringBuilder($"discover/tv?api_key={_config[API_KEY]}&page={parameters.Page}");

            if (!string.IsNullOrEmpty(parameters.SortOrder))
            {
                builder.AppendLine($"&sort_by={parameters.SortOrder}");
            }

            if (parameters.FirstAirYear is not 0)
            {
                builder.AppendLine($"&first_air_date_year={parameters.FirstAirYear}");
            }


            if (!string.IsNullOrEmpty(parameters.FromFirstAirDate))
            {
                builder.AppendLine($"&first_air_date.gte={parameters.FromFirstAirDate}");
            }

            if (!string.IsNullOrEmpty(parameters.ToFirstAirDate))
            {
                builder.AppendLine($"&first_air_date.lte={parameters.ToFirstAirDate}");
            }

            if (parameters.GenreIds.Count() is not 0)
            {
                builder.AppendLine("&with_genres=");

                foreach (var genreId in parameters.GenreIds)
                {
                    if (genreId == parameters.GenreIds.Last())
                    {
                        builder.AppendLine($"{genreId}");
                    }
                    else
                    {
                        builder.AppendLine($"{genreId},");
                    }
                }
            }

            if (parameters.Runtime is not 0)
            {
                builder.AppendLine($"&with_runtime.gte={parameters.Runtime}");
            }

            builder.AppendLine("&include_adult=false");

            return builder.ToString();
        }
    }
}
