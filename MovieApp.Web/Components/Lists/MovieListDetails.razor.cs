using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MovieApp.Domain.Models;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class MovieListDetails
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
        public MovieListDetailsDto List { get; set; } = new MovieListDetailsDto();
        public List<MovieDetails> Movies { get; set; } = new List<MovieDetails>();


        protected override async Task OnInitializedAsync()
        {
            var user = await UserService.GetUserAsync(int.Parse(UserId));
            var movieList = await ListService.GetMovieListAsync(int.Parse(MovieListId));

            User = user;
            List = movieList;

            foreach (var item in List.Items)
            {
                var movie = await MovieService.GetMovieDetailsAsync(item.MovieId);
                Movies.Add(movie);
            }
        }

        protected async Task HandleEditBtnClick()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            var parameters = new ModalParameters();
            parameters.Add(nameof(User), User);
            parameters.Add(nameof(List), List);

            var modal = Modal.Show<EditMovieListForm>("Edit List", parameters, options);
            var result = await modal.Result;

            if (!result.Cancelled && User != null)
            {
                var movieList = await ListService.GetMovieListAsync(int.Parse(MovieListId));
                List = movieList;

                StateHasChanged();
            }
        }

        protected async Task HandleDeleteBtnClick()
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
                var succeeded = await ListService.DeleteMovieListAsync(List.Id);

                if (succeeded)
                {
                    NavigationManager.NavigateTo($"/lists/{User.Id}");
                }
            }
        }
    }
}
