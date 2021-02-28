using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class Home
    {
        [Inject]
        public IMovieService MovieService { get; set; }

        [Inject]
        public ITVService TVService { get; set; }

        public IEnumerable<Movie> PopularMovies { get; set; } = new List<Movie>();

        public IEnumerable<TVShow> OnTheAirTVShows { get; set; } = new List<TVShow>();

        public IEnumerable<Movie> UpcomingMovies { get; set; } = new List<Movie>();

        protected override async Task OnInitializedAsync()
        {
            var popularMovies = await MovieService.GetPopularMoviesAsync();
            var onTheAirTvShows = await TVService.GetOnTheAirTVAsync();
            var upcomingMovies = await MovieService.GetUpcomingMoviesAsync();

            if (popularMovies is not null && upcomingMovies is not null && onTheAirTvShows is not null)
            {
                PopularMovies = popularMovies.Results;
                UpcomingMovies = upcomingMovies.Results;
                OnTheAirTVShows = onTheAirTvShows.Results;
            }
        }
    }
}
