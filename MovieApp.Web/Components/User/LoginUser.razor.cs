using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class LoginUser
    {
        [Inject]
        public IUserService UserService { get; set; }

        public LoginRequest LoginModel { get; set; } = new LoginRequest();

        public bool ShowResultMessage { get; set; }
        public string ResultMessage { get; set; }

        protected async Task HandleValidSubmit()
        {
            var response = await UserService.LoginUser(LoginModel);

            ShowResultMessage = true;

            if (response.Success)
            {
                ResultMessage = "Login successful.";
            }
            else
            {
                ResultMessage = "Login failed.";
            }
        }
    }
}
