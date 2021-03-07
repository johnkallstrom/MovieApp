using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class UserProfile
    {
        [Parameter]
        public string UserId { get; set; }

        //[Inject]
        //public IUserService UserService { get; set; }

        //protected override Task OnInitializedAsync()
        //{
        //    var user = await UserService.GetUserAsync();
        //}
    }
}
