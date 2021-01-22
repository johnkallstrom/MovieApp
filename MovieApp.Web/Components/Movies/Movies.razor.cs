using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Movies
{
    public partial class Movies
    {
        [Inject]
        public SearchState SearchState { get; set; }

        [Inject]
        public IMovieService MovieService { get; set; }

        public IEnumerable<Movie> SearchMovies { get; set; } = new List<Movie>();

        protected override void OnInitialized()
        {
            SearchState.OnChange += GetMovies;
        }

        private async void GetMovies()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.Query))
            {
               SearchMovies = await MovieService.GetMoviesBySearchAsync(new MovieParameters { Query = SearchState.Query });
               StateHasChanged();
            }
            else
            {
                SearchMovies = null;
                StateHasChanged();
            }
        }
    }
}
