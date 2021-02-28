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

        public bool ShowResultMessage { get; set; }
        public string ResultMessage { get; set; }

        protected async Task HandleValidSubmit()
        {
            var response = await AuthenticationService.RegisterUser(RegisterModel);

            ShowResultMessage = true;

            if (response.Success)
            {
                ResultMessage = "Registration successful.";
                NavigationManager.NavigateTo("/user/login");
            }
            else
            {
                ResultMessage = "Registration failed.";
            }
        }
    }
}
