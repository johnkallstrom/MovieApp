using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class EditUserForm
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Inject]
        public IUserHttpService UserService { get; set; }

        [Parameter]
        public UserDto User { get; set; }

        public UpdateUserRequest EditModel { get; set; } = new UpdateUserRequest();
        public bool DisplayLoadingSpinner { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = await UserService.GetUserAsync(User.Id);

            EditModel.FirstName = user.FirstName;
            EditModel.LastName = user.LastName;
            EditModel.Location = user.Location;
        }

        protected async Task HandleCancelAsync()
        {
            await ModalInstance.CancelAsync();
        }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await UserService.UpdateUserAsync(User.Id, EditModel);

            if (response.Success)
            {
                DisplayLoadingSpinner = false;
                await ModalInstance.CloseAsync();
            }
        }
    }
}
