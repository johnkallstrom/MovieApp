﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class LoginUser
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public LoginRequest LoginModel { get; set; } = new LoginRequest();

        public bool DisplayLoadingSpinner { get; set; }
        public bool DisplayMessage { get; set; }
        public string Message { get; set; }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await AuthenticationService.LoginUser(LoginModel);

            if (response.Success)
            {
                DisplayLoadingSpinner = false;
                NavigationManager.NavigateTo("/");
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
