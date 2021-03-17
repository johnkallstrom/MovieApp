using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
        #region Properties
        [Inject]
        public IMovieHttpService MovieService { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IConfiguration Config { get; set; }

        [Parameter]
        public string Id { get; set; }

        public MovieDetails Movie { get; set; } = new MovieDetails();

        public IEnumerable<Person> Cast { get; set; } = new List<Person>();

        public IEnumerable<Movie> SimilarMovies { get; set; } = new List<Movie>();
        #endregion

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

        //protected async Task HandleAddToFavBtnClick()
        //{
        //    var user = await GetUser();

        //    if (user.Identity.IsAuthenticated)
        //    {
        //        int userId = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid" || claim.Type == ClaimTypes.NameIdentifier).Value);

        //        var model = new AddFavoriteMovieDto
        //        {
        //            TmdbId = Movie.Id,
        //            Title = Movie.Title
        //        };

        //        var succeeded = await FavoriteService.AddMovieToFavorites(userId, model);

        //        if (succeeded)
        //        {
        //            ToastService.ShowSuccess($"{Movie.Title} has been added to your favorites list.");

        //            MovieExistsAsFavorite = true;
        //            StateHasChanged();
        //        }
        //    }
        //}

        //protected async Task HandleDeleteFromFavBtnClick()
        //{
        //    var user = await GetUser();

        //    if (user.Identity.IsAuthenticated)
        //    {
        //        int userId = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid" || claim.Type == ClaimTypes.NameIdentifier).Value);

        //        await FavoriteService.DeleteMovieFromFavorites(userId, (int)Movie.Id);

        //        ToastService.ShowError($"{Movie.Title} has been deleted from your favorites list.");

        //        MovieExistsAsFavorite = false;
        //        StateHasChanged();
        //    }
        //}

        //protected override async Task OnParametersSetAsync()
        //{
        //    var similarMovies = Enumerable.Empty<Movie>();
        //    var actors = Enumerable.Empty<Person>();

        //    if (int.TryParse(Id, out int movieId))
        //    {
        //        Movie = await MovieService.GetMovieDetailsAsync(movieId);
        //        actors = await MovieService.GetMovieCastAsync(movieId);
        //        similarMovies = await MovieService.GetSimilarMoviesAsync(movieId);
        //    }

        //    Cast = actors.Where(x => x.Known_For_Department == "Acting").Take(10);
        //    SimilarMovies = similarMovies.Take(10);
        //}

        #region Private Methods
        private async Task SetMovieInLocalStorage()
        {
            var movie = await LocalStorage.GetItemAsync<Movie>($"{Config["RecentlyViewed:MovieKey"]}{Movie.Id}");

            if (movie is null)
            {
                await LocalStorage.SetItemAsync($"{Config["RecentlyViewed:MovieKey"]}{Movie.Id}", Movie);
            }
        }
        #endregion
    }
}
