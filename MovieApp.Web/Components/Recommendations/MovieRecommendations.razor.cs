﻿using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class MovieRecommendations
    {
        [Inject]
        public IDiscoverService DiscoverService { get; set; }

        [Inject]
        public IGenreService GenreService { get; set; }

        public int Page { get; set; } = 1;
        public string SortOrder { get; set; }
        public int GenreId { get; set; } = 0;
        public int ReleaseYear { get; set; } = 0;
        public IEnumerable<Movie> Results { get; set; } = new List<Movie>();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var results = await FetchMovieResults();

            if (results is not null)
            {
                Page = results.Page;
                Results = results.Results;
                TotalPages = results.Total_Pages;
                TotalResults = results.Total_Results;
            }
        }

        protected async Task HandleButtonClick()
        {
            Page = 1;

            var results = await FetchMovieResults();

            if (results is not null)
            {
                Page = results.Page;
                Results = results.Results;
                TotalPages = results.Total_Pages;
                TotalResults = results.Total_Results;
            }
        }

        protected async Task HandlePageChanged(int selectedPage)
        {
            Page = selectedPage;

            var results = await FetchMovieResults();

            if (results is not null)
            {
                Page = results.Page;
                Results = results.Results;
                TotalPages = results.Total_Pages;
                TotalResults = results.Total_Results;
            }
        }

        protected void HandleSortSelection(string selectedSortOrder)
        {
            SortOrder = selectedSortOrder;
        }

        protected void HandleGenreSelection(string selectedGenre)
        {
            if (int.TryParse(selectedGenre, out int parsedGenreId))
            {
                GenreId = parsedGenreId;
            }
        }

        protected void HandleYearSelection(string selectedYear)
        {
            if (int.TryParse(selectedYear, out int parsedYear))
            {
                ReleaseYear = parsedYear;
            }
        }

        private async Task<MovieResults> FetchMovieResults()
        {
            var results = await DiscoverService.GetMoviesAsync(new MovieParameters { Page = Page, SortOrder = SortOrder, GenreId = GenreId, ReleaseYear = ReleaseYear });

            return results;
        }
    }
}