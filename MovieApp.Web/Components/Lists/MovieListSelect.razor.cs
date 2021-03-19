using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class MovieListSelect
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public IUserHttpService UserService { get; set; }

        public IEnumerable<MovieListDto> UserLists { get; set; } = new List<MovieListDto>();

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                int userId = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "nameid" || claim.Type == ClaimTypes.NameIdentifier).Value);

                UserLists = await UserService.GetUserMovieListsAsync(userId);
            }
        }
    }
}
