﻿using Microsoft.AspNetCore.Components;
using MovieApp.Web.Data;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class GenreSwitch
    {
        [Inject]
        public IGenreHttpService GenreService { get; set; }

        [Parameter]
        public bool ShowMovieOptions { get; set; } = false;

        [Parameter]
        public bool ShowTVOptions { get; set; } = false;

        [Parameter]
        public EventCallback<GenreSelectResult> OnGenreSelection { get; set; }

        public IEnumerable<Genre> MovieGenreOptions { get; set; } = new List<Genre>();
        public IEnumerable<Genre> TVGenreOptions { get; set; } = new List<Genre>();

        protected override async Task OnInitializedAsync()
        {
            if (ShowMovieOptions is true)
            {
                MovieGenreOptions = await GenreService.GetMovieGenresAsync();
            }

            if (ShowTVOptions is true)
            {
                TVGenreOptions = await GenreService.GetTVGenresAsync();
            }
        }

        private async Task HandleGenreSelection(ChangeEventArgs e, int genreId)
        {
            if (genreId is not 0)
            {
                await OnGenreSelection.InvokeAsync(new GenreSelectResult { Id = genreId, IsActive = (bool)e.Value });
            }
        }
    }
}
