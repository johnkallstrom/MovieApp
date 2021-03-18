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

        }
    }
}
