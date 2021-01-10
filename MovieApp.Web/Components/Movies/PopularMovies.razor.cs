using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Movies
{
    public partial class PopularMovies
    {
        [Inject]
        public IMovieService MovieService { get; set; }

        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();

        protected override async Task OnInitializedAsync()
        {
            Movies = await MovieService.GetPopularMoviesAsync();
        }
    }
}
