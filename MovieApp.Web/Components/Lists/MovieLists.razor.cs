using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class MovieLists
    {
        [Inject]
        public IUserHttpService UserService { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public UserDto User { get; set; } = new UserDto();

        protected override async Task OnInitializedAsync()
        {
            var user = await UserService.GetUserAsync(int.Parse(UserId));

            if (user is not null)
            {
                User = user;
            }
        }
    }
}
