using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class TVRecommendations
    {
        [Inject]
        public IDiscoverService DiscoverService { get; set; }

        [Inject]
        public IGenreService GenreService { get; set; }

        public int Page { get; set; } = 1;
        public string SortOrder { get; set; }
        public int GenreId { get; set; }
        public int FirstAirYear { get; set; } = 0;
        public IEnumerable<TVShow> Results { get; set; } = new List<TVShow>();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var results = await FetchTVResults();

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

            var results = await FetchTVResults();

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

            var results = await FetchTVResults();

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
                FirstAirYear = parsedYear;
            }
        }

        private async Task<TVResults> FetchTVResults()
        {
            var results = await DiscoverService.GetTVAsync(new TVParameters { Page = Page, SortOrder = SortOrder, GenreId = GenreId, FirstAirYear = FirstAirYear });

            return results;
        }
    }
}
