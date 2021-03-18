﻿using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class EditMovieListForm
    {
        [Inject]
        public IMovieListHttpService MovieListService { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public UserDto User { get; set; } = new UserDto();

        [Parameter]
        public MovieListDto List { get; set; } = new MovieListDto();

        public UpdateMovieListRequest EditListModel { get; set; } = new UpdateMovieListRequest();

        public bool DisplayLoadingSpinner { get; set; }
        public bool DisplayMessage { get; set; }
        public string Message { get; set; }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await MovieListService.UpdateMovieListAsync(User.Id, List.Id, EditListModel);

            if (response.Success)
            {
                DisplayLoadingSpinner = false;
                await ModalInstance.CloseAsync();
            }
            else
            {
                DisplayLoadingSpinner = false;
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