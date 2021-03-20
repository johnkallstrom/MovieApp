using Microsoft.AspNetCore.Components;
using MovieApp.Domain.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class CreateMovieList
    {
        [Inject]
        public IListHttpService MovieListService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public CreateMovieListRequest CreateListModel { get; set; } = new CreateMovieListRequest();

        public bool DisplayLoadingSpinner { get; set; }

        protected async Task HandleValidSubmit()
        {
            DisplayLoadingSpinner = true;

            var response = await MovieListService.CreateMovieListAsync(int.Parse(UserId), CreateListModel);

            if (response.Success)
            {
                DisplayLoadingSpinner = false;
                NavigationManager.NavigateTo($"/lists/{UserId}");
            }
            else
            {
                DisplayLoadingSpinner = false;
            }
        }
    }
}
