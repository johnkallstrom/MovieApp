using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class EditUserForm
    {
        [Inject]
        public IUserHttpService UserService { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        public UpdateUserDto EditModel { get; set; } = new UpdateUserDto();

        [Parameter]
        public UserDto User { get; set; } = new UserDto();

        public bool DisplayLoadingSpinner { get; set; }
        public bool DisplayMessage { get; set; }
        public string Message { get; set; }

        protected override void OnInitialized()
        {
            EditModel.FirstName = User.FirstName;
            EditModel.LastName = User.LastName;
            EditModel.Location = User.Location;
            EditModel.Bio = User.Bio;
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
            else
            {
                DisplayLoadingSpinner = true;
                Message = response.Message;
                DisplayMessage = true;
            }
        }

        protected async Task HandleCancelModal()
        {
            await ModalInstance.CancelAsync();
        }
    }
}
