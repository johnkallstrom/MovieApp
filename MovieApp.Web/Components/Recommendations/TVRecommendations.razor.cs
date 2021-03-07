using Microsoft.AspNetCore.Components;
using MovieApp.Web.Data;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class TVRecommendations
    {
        [Inject]
        public IDiscoverHttpService DiscoverService { get; set; }

        [Inject]
        public IGenreHttpService GenreService { get; set; }

        public int Page { get; set; } = 1;
        public string SortOrder { get; set; }
        public int GenreId { get; set; }
        public int FirstAirYear { get; set; } = 0;
        public string SearchQuery { get; set; }
        public IEnumerable<TVShow> Results { get; set; } = new List<TVShow>();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public string FromFirstAirDate { get; set; }
        public string ToFirstAirDate { get; set; }
        public int Runtime { get; set; }
        public List<int> SelectedGenreIds { get; set; } = new List<int>();

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

        protected void HandleDateSelection(DateSelectResult result)
        {
            if (result is not null)
            {
                if (result.Type == "from")
                    FromFirstAirDate = result.Value;

                if (result.Type == "to")
                    ToFirstAirDate = result.Value;
            }
        }

        protected void HandleRuntimeChanged(string selectedRuntime)
        {
            if (int.TryParse(selectedRuntime, out int parsedRuntime))
            {
                Runtime = parsedRuntime;
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

        protected void HandleSortSelection(string selectedSortOrder) => SortOrder = selectedSortOrder;

        protected void HandleGenreSelection(GenreSelectResult result)
        {
            if (result is not null)
            {
                if (result.IsActive is true)
                {
                    SelectedGenreIds.Add(result.Id);
                }

                if (result.IsActive is not true)
                {
                    SelectedGenreIds.Remove(result.Id);
                }
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
            var parameters = new TVParameters
            {
                Page = Page,
                SortOrder = SortOrder,
                FirstAirYear = FirstAirYear,
                FromFirstAirDate = FromFirstAirDate,
                ToFirstAirDate = ToFirstAirDate,
                Runtime = Runtime
            };

            if (SelectedGenreIds.Count() is not 0)
            {
                parameters.GenreIds = SelectedGenreIds;
            }

            var results = await DiscoverService.GetTVAsync(parameters);

            return results;
        }
    }
}