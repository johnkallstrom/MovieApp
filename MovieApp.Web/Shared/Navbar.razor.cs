using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System.Threading.Tasks;

namespace MovieApp.Web.Shared
{
    public partial class Navbar
    {
        [Inject]
        public SearchState SearchState { get; set; }

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task HandleLogout()
        {
            SearchState.ClearQuery();
            await AuthenticationService.LogoutUser();
            NavigationManager.NavigateTo("/user/login");
        }
    }
}
