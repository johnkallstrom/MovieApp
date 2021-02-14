using Microsoft.AspNetCore.Components;
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
        public IPeopleService PeopleService { get; set; }

        [Inject]
        public IDiscoverService DiscoverService { get; set; }

        [Inject]
        public IGenreService GenreService { get; set; }

        public int Page { get; set; } = 1;
        public string SortOrder { get; set; }
        public int GenreId { get; set; } = 0;
        public int ReleaseYear { get; set; } = 0;
        public IEnumerable<Movie> Results { get; set; } = new List<Movie>();
        public IEnumerable<Person> SearchResults { get; set; } = new List<Person>();
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

        protected async Task HandleQueryChange(string query)
        {
            var results = await FetchCastResults(query);

            SearchResults = results.Where(person => person.Known_For_Department == "Acting").ToList();

            foreach (var person in SearchResults)
            {
                Console.WriteLine($"Name: {person.Name}");
                Console.WriteLine($"Known For Department: {person.Known_For_Department}");
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

        private async Task<IEnumerable<Person>> FetchCastResults(string query)
        {
            var data = await PeopleService.GetPeopleBySearchAsync(query);

            return data.Results;
        }
    }
}
