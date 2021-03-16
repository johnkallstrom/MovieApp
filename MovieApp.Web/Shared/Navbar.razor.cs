using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using MovieApp.Web.Components.User;
using MovieApp.Web.State;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Web.Shared
{
    public partial class Navbar
    {
        [Inject]
        public IConfiguration Config { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public SearchState SearchState { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        public string UserEmail { get; set; }
        public int UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetAuthenticatedUserClaims();
        }

        protected async Task HandleLogoutClick()
        {
            SearchState.Clear();

            int storageLength = await LocalStorage.LengthAsync();

            if (storageLength >= 1)
            {
                for (int index = 0; index < storageLength; index++)
                {
                    string key = await LocalStorage.KeyAsync(index);
                    if (key.Contains(Config["RecentlyViewed:MovieKey"]) || key.Contains(Config["RecentlyViewed:TVKey"]))
                    {
                        await LocalStorage.RemoveItemAsync(key);
                    }
                }
            }
        }

        protected async Task HandleLoginClick()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            var modal = Modal.Show<LoginForm>("Login User", options);
            var result = await modal.Result;

            if (!result.Cancelled)
            {
                await GetAuthenticatedUserClaims();
            }
        }

        protected async Task HandleRegisterClick()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            var modal = Modal.Show<RegisterForm>("Register User", options);
            var result = await modal.Result;

            if (!result.Cancelled)
            {
                await GetAuthenticatedUserClaims();
            }
        }

        private async Task GetAuthenticatedUserClaims()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            if (authState.User.Identity.IsAuthenticated)
            {
                UserId = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid" || claim.Type == ClaimTypes.NameIdentifier).Value);
                UserEmail = user.Claims.FirstOrDefault(claim => claim.Type == "email" || claim.Type == ClaimTypes.Email).Value;

                StateHasChanged();
            }
        }
    }
}