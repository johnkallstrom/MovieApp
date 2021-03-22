using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class EditMovieList
    {
        [Inject]
        public IListHttpService MovieListService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string MovieListId { get; set; }

        public UpdateMovieListRequest EditListModel { get; set; } = new UpdateMovieListRequest();

        public bool DisplayLoadingSpinner { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var movieList = await MovieListService.GetMovieListAsync(int.Parse(MovieListId));

            EditListModel.Name = movieList.Name;
            EditListModel.Description = movieList.Description;
        }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await MovieListService.UpdateMovieListAsync(int.Parse(MovieListId), EditListModel);

            if (response.Success)
            {
                DisplayLoadingSpinner = false;
                NavigationManager.NavigateTo($"/user/profile/{UserId}");
            }
        }

        protected async Task HandleDeleteItemButton(int tmdbId)
        {
            var request = new DeleteMovieItemRequest()
            {
                TmdbId = tmdbId
            };

            var response = await MovieListService.DeleteMovieFromListAsync(int.Parse(MovieListId), request);

            if (response.Success)
            {
                var movieList = await MovieListService.GetMovieListAsync(int.Parse(MovieListId));

                EditListModel.Name = movieList.Name;
                EditListModel.Description = movieList.Description;

                StateHasChanged();
            }
        }
    }
}
