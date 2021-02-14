using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Movies
{
    public partial class Movies
    {
        [Inject]
        public IMovieService MovieService { get; set; }


        public IEnumerable<Movie> PopularMovies { get; set; } = new List<Movie>();


        protected override async Task OnInitializedAsync()
        {
            var data = await MovieService.GetPopularMoviesAsync();

            if (data is not null)
            {
                PopularMovies = data.Results;
            }
        }
    }
}
