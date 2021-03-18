using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Components.Lists;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class MovieLists
    {
        [Inject]
        public IListHttpService MovieListService { get; set; }

        [Inject]
        public IUserHttpService UserService { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public UserDto User { get; set; } = new UserDto();

        public IEnumerable<MovieListDto> Lists { get; set; } = new List<MovieListDto>();

        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(UserId, out int parsedId))
            {
                var user = await UserService.GetUserAsync(parsedId);

                if (user is not null)
                {
                    User = user;
                }

                Lists = await UserService.GetUserMovieListsAsync(parsedId);
            }
            else
            {
                User = null;
            }
        }

        protected async Task HandleCreateListBtnClick()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            var parameters = new ModalParameters();
            parameters.Add(nameof(User), User);

            var modal = Modal.Show<CreateMovieListForm>("New List", parameters, options);
            var result = await modal.Result;

            if (!result.Cancelled && User != null)
            {
                Lists = await UserService.GetUserMovieListsAsync(User.Id);

                StateHasChanged();
            }
        }
    }
}
