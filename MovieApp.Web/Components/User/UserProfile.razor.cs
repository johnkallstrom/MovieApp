using Microsoft.AspNetCore.Components;

namespace MovieApp.Web.Components.User
{
    public partial class UserProfile
    {
        [Parameter]
        public string UserId { get; set; }
    }
}
