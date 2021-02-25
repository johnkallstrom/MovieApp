using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class RegisterUser
    {
        [Inject]
        public IUserService UserService { get; set; }

        public RegisterRequest RegisterModel { get; set; } = new RegisterRequest();

        public bool ShowResultMessage { get; set; }
        public string ResultMessage { get; set; }

        protected async Task HandleValidSubmit()
        {
            var response = await UserService.RegisterUser(RegisterModel);

            ShowResultMessage = true;

            if (response.Success)
            {
                ResultMessage = "Registration successful.";
            }
            else
            {
                ResultMessage = "Registration failed.";
            }
        }
    }
}
