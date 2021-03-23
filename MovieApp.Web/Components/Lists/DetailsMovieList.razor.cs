using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class DetailsMovieList
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Inject]
        public IUserHttpService UserService { get; set; }

        [Inject]
        public IMovieHttpService MovieService { get; set; }

        [Inject]
        public IListHttpService ListService { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string MovieListId { get; set; }

        public UserDto User { get; set; } = new UserDto();
        public MovieListDto MovieList { get; set; } = new MovieListDto();
        public List<MovieDetails> Movies { get; set; } = new List<MovieDetails>();

        protected override async Task OnInitializedAsync()
        {
            var user = await UserService.GetUserAsync(int.Parse(UserId));
            var movieList = await ListService.GetMovieListAsync(int.Parse(MovieListId));

            User = user;
            MovieList = movieList;

            foreach (var movieItem in MovieList.Movies)
            {
                var movie = await MovieService.GetMovieDetailsAsync(movieItem.TmdbId);
                Movies.Add(movie);
            }
        }

        protected async Task HandleEditListButton()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            var parameters = new ModalParameters();
            parameters.Add(nameof(User), User);
            parameters.Add(nameof(MovieList), MovieList);

            var modal = Modal.Show<EditMovieListForm>("Edit List", parameters, options);
            var result = await modal.Result;

            if (!result.Cancelled && MovieList != null)
            {
                var movieList = await ListService.GetMovieListAsync(MovieList.Id);
                MovieList = movieList;

                StateHasChanged();
            }
        }

        protected async Task HandleDeleteListButton()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            var modal = Modal.Show<DeleteListConfirmation>("Delete List", options);
            var result = await modal.Result;

            if (!result.Cancelled && User != null)
            {
                var succeeded = await ListService.DeleteMovieListAsync(MovieList.Id);

                if (succeeded)
                {
                    NavigationManager.NavigateTo($"/user/profile/{User.Id}");
                }
            }
        }
    }
}
