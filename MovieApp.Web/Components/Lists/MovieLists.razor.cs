using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class MovieLists
    {
        [Inject]
        public IUserHttpService UserService { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Parameter]
        public UserDto User { get; set; } = new UserDto();

        protected async Task HandleCreateListButton()
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
                var user = await UserService.GetUserAsync(User.Id);

                User = user;
            }
        }
    }
}
