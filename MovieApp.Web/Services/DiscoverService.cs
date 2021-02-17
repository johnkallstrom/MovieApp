﻿using Microsoft.Extensions.Configuration;
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
    public class DiscoverService : IDiscoverService
    {
        private const string API_KEY = "ApiKey";
        private const string IMAGE_URL = "ImageBaseUrl";
        private const string PLACEHOLDER_IMAGE_URL = "PlaceholderImageBaseUrl";

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public DiscoverService(
            IConfiguration config,
            HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<MovieResults> GetMoviesAsync(MovieParameters parameters)
        {
            string url = GenerateMovieApiUrl(parameters);

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
            string url = GenerateTVApiUrl(parameters);

            var data = await _httpClient.GetFromJsonAsync<TVResults>(url);

            string placeholderUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(_config[PLACEHOLDER_IMAGE_URL], 500, 750));

            foreach (var tvShow in data.Results)
            {
                tvShow.Poster_Path = !string.IsNullOrEmpty(tvShow.Poster_Path) ? ImageHelper.GetImageUrl(new ImageSettings(_config[IMAGE_URL], PosterSizeType.W500, tvShow.Poster_Path)) : placeholderUrl;
            }

            return data;
        }

        private string GenerateMovieApiUrl(MovieParameters parameters)
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

            builder.AppendLine("&include_adult=false");

            return builder.ToString();
        }

        private string GenerateTVApiUrl(TVParameters parameters)
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

            builder.AppendLine("&include_adult=false");

            return builder.ToString();
        }
    }
}
