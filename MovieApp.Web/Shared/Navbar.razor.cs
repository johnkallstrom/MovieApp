using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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

        protected void HandleLogoutClick()
        {
            SearchState.Clear();
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