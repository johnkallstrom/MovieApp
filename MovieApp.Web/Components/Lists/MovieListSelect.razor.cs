using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MovieApp.Domain.Models;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class MovieListSelect
    {
        [Inject]
        public IToastService ToastService { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public IListHttpService ListService { get; set; }

        [Inject]
        public IUserHttpService UserService { get; set; }

        [Parameter]
        public MovieDetails Movie { get; set; } = new MovieDetails();

        public IEnumerable<MovieListDto> UserLists { get; set; } = new List<MovieListDto>();

        public int SelectedListId { get; set; } = 0;
        public bool DisplayLoadingSpinner { get; set; }
        public bool DisplayMessage { get; set; }
        public string Message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                int userId = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid" || claim.Type == ClaimTypes.NameIdentifier).Value);

                UserLists = await UserService.GetUserMovieListsAsync(userId);
            }
        }

        protected async Task HandleAddButtonClick()
        {
            if (SelectedListId > 0)
            {
                DisplayLoadingSpinner = true;

                var request = new AddMovieRequest
                {
                    MovieId = Movie.Id,
                    Name = Movie.Title
                };

                var response = await ListService.AddMovieToListAsync(SelectedListId, request);

                if (response.Success)
                {
                    DisplayLoadingSpinner = false;
                    ToastService.ShowSuccess(response.Message);
                }
                else
                {
                    DisplayLoadingSpinner = false;
                    Message = response.Message;
                    DisplayMessage = true;
                }
            }
        }
    }
}
