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

        [Inject]
        public SearchService SearchService { get; set; }

        public IEnumerable<Movie> SearchMovies { get; set; } = new List<Movie>();

        protected override void OnInitialized()
        {
            SearchService.OnQueryChange += StateHasChanged;
        }
    }
}
