using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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

        public string UserEmail { get; set; }
        public int UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            if (authState.User.Identity.IsAuthenticated)
            {
                UserEmail = user.Claims.FirstOrDefault(claim => claim.Type == "email" || claim.Type == ClaimTypes.Email).Value;
                UserId = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid" || claim.Type == ClaimTypes.NameIdentifier).Value);
            }
        }
    }
}