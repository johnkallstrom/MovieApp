using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class MovieList
    {
        [Inject]
        public IMovieService MovieService { get; set; }

        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();

        protected override async Task OnInitializedAsync()
        {
            Movies = await MovieService.GetTopRatedMoviesAsync();
        }
    }
}
