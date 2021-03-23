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
        public IListHttpService MovieListService { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public UserDto User { get; set; }

        public CreateMovieListRequest CreateListModel { get; set; } = new CreateMovieListRequest();

        public bool DisplayLoadingSpinner { get; set; }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await MovieListService.CreateMovieListAsync(User.Id, CreateListModel);

            if (response.Success)
            {
                DisplayLoadingSpinner = false;
                await ModalInstance.CloseAsync();
            }
        }

        protected async Task HandleCancelAsync()
        {
            await ModalInstance.CancelAsync();
        }
    }
}
