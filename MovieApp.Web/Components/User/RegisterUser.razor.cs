using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class RegisterUser
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

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
                NavigationManager.NavigateTo("/user/login");
            }
            else
            {
                DisplayMessage = true;
                Message = response.Message;
                DisplayLoadingSpinner = false;
            }
        }
    }
}
