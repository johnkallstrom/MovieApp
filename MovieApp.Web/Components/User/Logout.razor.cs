using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class Logout
    {
        // To do: Authorize this page

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.LogoutUser();
        }
    }
}
