using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Person> Cast { get; set; } = new List<Person>();

        protected override async Task OnInitializedAsync()
        {
            var actors = Enumerable.Empty<Person>();

            if (int.TryParse(Id, out int movieId))
            {
                Movie = await MovieService.GetMovieDetailsAsync(movieId);
                actors = await MovieService.GetMovieCastAsync(movieId);
            }

            Cast = actors.Where(x => x.Known_For_Department == "Acting").Take(10);
        }
    }
}
