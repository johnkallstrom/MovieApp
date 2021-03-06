﻿using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class Home
    {
        [Inject]
        public IMovieHttpService MovieService { get; set; }

        [Inject]
        public ITVHttpService TVService { get; set; }

        public IEnumerable<Movie> UpcomingMovies { get; set; } = new List<Movie>();
        public IEnumerable<TVShowDetails> PopularTVShows { get; set; } = new List<TVShowDetails>();

        protected override async Task OnInitializedAsync()
        {
            var movieResults = await MovieService.GetUpcomingMoviesAsync();
            var tvResults = await TVService.GetPopularTVAsync();

            if (movieResults is not null && tvResults is not null)
            {
                UpcomingMovies = movieResults.Results;
                PopularTVShows = tvResults.Results;
            }
        }
    }
}
