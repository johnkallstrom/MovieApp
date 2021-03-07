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

        [Parameter]
        public string UserId { get; set; }

        public UserDto User { get; set; } = new UserDto();

        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(UserId, out int parsedId))
            {
                User = await UserService.GetUserAsync(parsedId);
            }
            else
            {
                User = null;
            }
        }
    }
}
