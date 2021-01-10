using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Movies
{
    public partial class UpcomingMovies
    {
        [Inject]
        public IMovieService MovieService { get; set; }

        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();

        protected override async Task OnInitializedAsync()
        {
            Movies = await MovieService.GetUpcomingMoviesAsync();
        }
    }
}
