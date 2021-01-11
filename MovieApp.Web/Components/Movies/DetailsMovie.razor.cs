using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Movies
{
    public partial class DetailsMovie
    {
        [Inject]
        public IMovieService MovieService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public MovieDetails Movie { get; set; } = new MovieDetails();

        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(Id, out int result))
            {
                Movie = await MovieService.GetMovieDetailsAsync(result);
            }
        }
    }
}
