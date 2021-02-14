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
        public bool ShowMovieOptions { get; set; } = false;

        [Parameter]
        public bool ShowTVOptions { get; set; } = false;

        [Parameter]
        public EventCallback<string> OnGenreSelection { get; set; }

        public IEnumerable<Genre> MovieGenreOptions { get; set; } = new List<Genre>();
        public IEnumerable<Genre> TVGenreOptions { get; set; } = new List<Genre>();

        protected override async Task OnInitializedAsync()
        {
            if (ShowMovieOptions)
                MovieGenreOptions = await GenreService.GetMovieGenresAsync();

            if (ShowTVOptions)
                TVGenreOptions = await GenreService.GetTVGenresAsync();
        }
    }
}
