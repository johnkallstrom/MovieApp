﻿using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.User
{
    public partial class UserProfile
    {
        [Inject]
        public IUserHttpService UserService { get; set; }

        [Inject]
        public IAuthenticationHttpService AuthenticateService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        public UserDto User { get; set; } = new UserDto();
        public bool DisplayLoadingSpinner { get; set; }

        protected override async Task OnInitializedAsync()
        {
            DisplayLoadingSpinner = true;

            if (int.TryParse(UserId, out int parsedId))
            {
                var user = await UserService.GetUserAsync(parsedId);

                if (user is not null)
                {
                    User = user;
                    DisplayLoadingSpinner = false;
                }
            }
        }

        protected async Task HandleEditButton()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            var parameters = new ModalParameters();
            parameters.Add(nameof(User), User);

            var modal = Modal.Show<EditUserForm>("Edit User", parameters, options);
            var result = await modal.Result;

            if (!result.Cancelled && User != null)
            {
                var user = await UserService.GetUserAsync(int.Parse(UserId));

                User = user;
                StateHasChanged();
            }
        }

        protected async Task HandleDeleteButton()
        {
            var options = new ModalOptions()
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true
            };

            var modal = Modal.Show<DeleteUserConfirmation>("Delete User", options);
            var result = await modal.Result;

            if (!result.Cancelled && User != null)
            {
                await AuthenticateService.LogoutUser();

                var succeeded = await UserService.DeleteUserAsync(User.Id);
                if (succeeded)
                {
                    NavigationManager.NavigateTo("/");
                }
            }
        }
    }
}
