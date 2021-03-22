using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;

namespace MovieApp.Web.Components.Lists
{
    public partial class MovieLists
    {
        [Inject]
        public IUserHttpService UserService { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Parameter]
        public UserDto User { get; set; } = new UserDto();
    }
}
