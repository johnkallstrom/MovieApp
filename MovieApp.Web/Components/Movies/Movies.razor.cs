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
        public IEnumerable<Movie> TopRatedMovies { get; set; } = new List<Movie>();
        public IEnumerable<Movie> UpcomingMovies { get; set; } = new List<Movie>();

        protected override async Task OnInitializedAsync()
        {
            var popularMovies = await MovieService.GetPopularMoviesAsync();
            var topRatedMovies = await MovieService.GetTopRatedMoviesAsync();
            var upcomingMovies = await MovieService.GetUpcomingMoviesAsync();

            if (popularMovies is not null && TopRatedMovies is not null && upcomingMovies is not null)
            {
                PopularMovies = popularMovies.Results;
                TopRatedMovies = topRatedMovies.Results;
                UpcomingMovies = upcomingMovies.Results;
            }
        }
    }
}
