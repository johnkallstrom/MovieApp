using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class GenreSelect
    {
        [Inject]
        public IGenreService GenreService { get; set; }

        [Parameter]
        public EventCallback<string> OnGenreSelection { get; set; }

        public IEnumerable<Genre> MovieGenreOptions { get; set; } = new List<Genre>();

        protected override async Task OnInitializedAsync()
        {
            MovieGenreOptions = await GenreService.GetMovieGenresAsync();
            await OnGenreSelection.InvokeAsync(MovieGenreOptions.First().Id.ToString());
        }
    }
}
