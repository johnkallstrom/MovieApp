using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class UserProfile
    {
        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                // redirect to login if not authenticated
                NavigationManager.NavigateTo("/user/login");
            }
        }

    }
}
