using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Movies
{
    public partial class PopularMovies
    {
        [Inject]
        public IMovieService MovieService { get; set; }

        public IEnumerable<Movie> Results { get; set; } = new List<Movie>();

        public int Page { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var data = await MovieService.GetPopularMoviesAsync(Page);

            SetMovieData(data);
        }

        protected async Task HandlePageChanged(int selectedPage)
        {
            Page = selectedPage;

            var data = await MovieService.GetPopularMoviesAsync(Page);

            SetMovieData(data);
        }

        private void SetMovieData(MovieResults data)
        {
            if (data is not null)
            {
                Results = data.Results;
                Page = data.Page;
                TotalPages = data.Total_Pages;
                TotalResults = data.Total_Results;
            }
        }
    }
}
