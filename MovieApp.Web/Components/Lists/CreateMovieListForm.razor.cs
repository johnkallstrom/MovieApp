using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class CreateMovieListForm
    {
        [Inject]
        public IMovieListHttpService MovieListService { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public UserDto User { get; set; } = new UserDto();

        public CreateMovieListRequest CreateListModel { get; set; } = new CreateMovieListRequest();

        public bool DisplayLoadingSpinner { get; set; }
        public bool DisplayMessage { get; set; }
        public string Message { get; set; }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await MovieListService.CreateMovieListAsync(User.Id, CreateListModel);

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
