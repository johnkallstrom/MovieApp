using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class UserProfile
    {
        [Inject]
        public IUserHttpService UserService { get; set; }

        [Inject]
        public IAuthenticationHttpService AuthenticateService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        public UserDto User { get; set; } = new UserDto();

        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(UserId, out int parsedId))
            {
                var user = await UserService.GetUserAsync(parsedId);

                if (user is not null)
                {
                    User = user;
                }
            }
            else
            {
                User = null;
            }
        }

        protected async Task HandleDeleteBtnClick()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            var modal = Modal.Show<DeleteUserConfirmation>("Delete User", options);
            var result = await modal.Result;

            if (!result.Cancelled && User != null)
            {
                await AuthenticateService.LogoutUser();

                var succeeded = await UserService.DeleteUserAsync(User.Id);
                if (succeeded)
                {
                    NavigationManager.NavigateTo("/");
                }
            }
        }
    }
}
