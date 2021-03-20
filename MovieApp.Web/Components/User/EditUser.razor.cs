using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class EditUser
    {
        [Inject]
        public IUserHttpService UserService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public UpdateUserRequest EditModel { get; set; } = new UpdateUserRequest();
        public bool DisplayLoadingSpinner { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = await UserService.GetUserAsync(int.Parse(UserId));

            EditModel.FirstName = user.FirstName;
            EditModel.LastName = user.LastName;
            EditModel.Location = user.Location;
            EditModel.Bio = user.Bio;
        }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await UserService.UpdateUserAsync(int.Parse(UserId), EditModel);

            if (response.Success)
            {
                DisplayLoadingSpinner = false;
                NavigationManager.NavigateTo($"/user/profile/{UserId}");
            }
        }
    }
}
