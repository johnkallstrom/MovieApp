using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MovieApp.Domain.Models;
using MovieApp.Web.Helpers;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class MovieListDetails
    {
        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Inject]
        public IConfiguration Config { get; set; }

        [Inject]
        public IUserHttpService UserService { get; set; }

        [Inject]
        public IMovieListHttpService MovieListService { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string MovieListId { get; set; }

        public UserDto User { get; set; } = new UserDto();
        public MovieListDto List { get; set; } = new MovieListDto();

        public string PlaceholderImageUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = await UserService.GetUserAsync(int.Parse(UserId));
            var movieList = await MovieListService.GetMovieListAsync(int.Parse(UserId), int.Parse(MovieListId));

            User = user;
            List = movieList;

            PlaceholderImageUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(Config["TMDB:PlaceholderImageBaseUrl"], 500, 750));
        }

        protected async Task HandleEditBtnClick()
        {
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
                var movieList = await MovieListService.GetMovieListAsync(int.Parse(UserId), int.Parse(MovieListId));
                List = movieList;

                StateHasChanged();
            }
        }
    }
}
