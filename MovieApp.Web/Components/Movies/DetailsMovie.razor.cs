using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using MovieApp.Domain.Models;
using MovieApp.Web.Components.User;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Movies
{
    public partial class DetailsMovie
    {
        #region Properties
        [Inject]
        public IFavoriteHttpService FavoriteService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public IMovieHttpService MovieService { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IConfiguration Config { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Parameter]
        public string Id { get; set; }

        public MovieDetails Movie { get; set; } = new MovieDetails();

        public IEnumerable<Person> Cast { get; set; } = new List<Person>();

        public IEnumerable<Movie> SimilarMovies { get; set; } = new List<Movie>();
        public bool DisplayLoading { get; set; }

        public bool MovieExistsAsFavorite { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            DisplayLoading = true;

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

            var user = await GetAuthenticatedUser();

            if (user.Identity.IsAuthenticated)
            {
                int userId = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid" || claim.Type == ClaimTypes.NameIdentifier).Value);
                
                MovieExistsAsFavorite = await FavoriteService.MovieExistsAsFavorite(userId, Movie.Id);
            }

            await SetMovieInLocalStorage();

            DisplayLoading = false;
        }

        protected async Task HandleAddToFavBtnClick()
        {
            var user = await GetAuthenticatedUser();

            if (user.Identity.IsAuthenticated)
            {
                int userId = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid" || claim.Type == ClaimTypes.NameIdentifier).Value);

                var model = new AddFavoriteMovieDto
                {
                    TmdbId = Movie.Id,
                    Title = Movie.Title
                };

                var succeeded = await FavoriteService.AddMovieToFavorites(userId, model);

                if (succeeded)
                {
                    ToastService.ShowSuccess($"{Movie.Title} has been added to your favorites list.");

                    MovieExistsAsFavorite = true;
                    StateHasChanged();
                }
            }
        }

        protected async Task HandleDeleteFromFavBtnClick()
        {
            var user = await GetAuthenticatedUser();

            if (user.Identity.IsAuthenticated)
            {
                int userId = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid" || claim.Type == ClaimTypes.NameIdentifier).Value);

                await FavoriteService.DeleteMovieFromFavorites(userId, Movie.Id);

                ToastService.ShowError($"{Movie.Title} has been deleted from your favorites list.");

                MovieExistsAsFavorite = false;
                StateHasChanged();
            }
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

        #region Private Methods
        private async Task<ClaimsPrincipal> GetAuthenticatedUser()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            return user;
        }

        private async Task SetMovieInLocalStorage()
        {
            var movie = await LocalStorage.GetItemAsync<MovieDetails>($"{Config["RecentlyViewed:MovieKey"]}{Movie.Id}");

            if (movie is null)
            {
                await LocalStorage.SetItemAsync($"{Config["RecentlyViewed:MovieKey"]}{Movie.Id}", Movie);
            }
        }
        #endregion
    }
}
