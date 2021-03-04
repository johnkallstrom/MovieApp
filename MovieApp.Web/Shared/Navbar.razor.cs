using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MovieApp.Web.Components.User;
using MovieApp.Web.State;

namespace MovieApp.Web.Shared
{
    public partial class Navbar
    {
        [Inject]
        public SearchState SearchState { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        protected void ShowLoginModal()
        {
            Modal.Show<LoginUser>("Login User");
        }
    }
}