﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Movies
{
    public partial class DetailsMovie
    {
        [Inject]
        public IMovieHttpService MovieService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public MovieDetails Movie { get; set; } = new MovieDetails();

        public IEnumerable<Person> Cast { get; set; } = new List<Person>();

        public IEnumerable<Movie> SimilarMovies { get; set; } = new List<Movie>();

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IConfiguration Config { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var similarMovies = Enumerable.Empty<Movie>();
            var actors = Enumerable.Empty<Person>();

            if (int.TryParse(Id, out int movieId))
            {
                Movie = await MovieService.GetMovieDetailsAsync(movieId);
                actors = await MovieService.GetMovieCastAsync(movieId);
                similarMovies = await MovieService.GetSimilarMoviesAsync(movieId);
            }

            Cast = actors.Where(x => x.Known_For_Department == "Acting").Take(10);
            SimilarMovies = similarMovies.Take(10);
            
            await SetMovieInLocalStorage();
        }

        protected async Task HandleFavoriteBtnClick()
        {

        }

        protected override async Task OnParametersSetAsync()
        {
            var similarMovies = Enumerable.Empty<Movie>();
            var actors = Enumerable.Empty<Person>();

            if (int.TryParse(Id, out int movieId))
            {
                Movie = await MovieService.GetMovieDetailsAsync(movieId);
                actors = await MovieService.GetMovieCastAsync(movieId);
                similarMovies = await MovieService.GetSimilarMoviesAsync(movieId);
            }

            Cast = actors.Where(x => x.Known_For_Department == "Acting").Take(10);
            SimilarMovies = similarMovies.Take(10);
        }

        private async Task SetMovieInLocalStorage()
        {
            var movie = await LocalStorage.GetItemAsync<MovieDetails>($"{Config["RecentlyViewed:MovieKey"]}{Movie.Id}");

            if (movie is null)
            {
                await LocalStorage.SetItemAsync($"{Config["RecentlyViewed:MovieKey"]}{Movie.Id}", Movie);
            }
        }
    }
}
