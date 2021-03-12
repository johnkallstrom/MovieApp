using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class RegisterForm
    {
        [Inject]
        public IAuthenticationHttpService AuthenticationService { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        public RegisterRequest RegisterModel { get; set; } = new RegisterRequest();

        public bool DisplayLoadingSpinner { get; set; }
        public bool DisplayMessage { get; set; }
        public string Message { get; set; }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await AuthenticationService.RegisterUser(RegisterModel);

            if (response.Success)
            {
                DisplayLoadingSpinner = false;

                var loginResponse = await AuthenticationService.LoginUser(new LoginRequest { Email = response.Email, Password = response.Password });

                if (loginResponse.Success)
                {
                    await ModalInstance.CloseAsync();
                }
            }
            else
            {
                DisplayMessage = true;
                Message = response.Message;
                DisplayLoadingSpinner = false;
            }
        }

        protected async Task HandleCancelModal()
        {
            await ModalInstance.CancelAsync();
        }
    }
}
