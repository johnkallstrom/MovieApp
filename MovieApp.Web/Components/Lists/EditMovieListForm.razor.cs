using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class EditMovieListForm
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Inject]
        public IListHttpService MovieListService { get; set; }

        [Parameter]
        public UserDto User { get; set; }

        [Parameter]
        public MovieListDto MovieList { get; set; }

        public UpdateMovieListRequest EditListModel { get; set; } = new UpdateMovieListRequest();

        public bool DisplayLoadingSpinner { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var movieList = await MovieListService.GetMovieListAsync(MovieList.Id);

            EditListModel.Name = movieList.Name;
            EditListModel.Description = movieList.Description;
        }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await MovieListService.UpdateMovieListAsync(MovieList.Id, EditListModel);

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
