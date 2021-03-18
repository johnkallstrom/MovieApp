using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Lists
{
    public partial class MovieListDetails
    {
        [Inject]
        public IMovieListHttpService MovieListService { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string MovieListId { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }
    }
}
