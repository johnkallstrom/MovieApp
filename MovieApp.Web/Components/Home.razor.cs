using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System.Collections.Generic;

namespace MovieApp.Web.Components
{
    public partial class Home
    {
        [Inject]
        public SearchState SearchState { get; set; }

        [Inject]
        public ISearchService SearchService { get; set; }

        public IEnumerable<Media> SearchResults { get; set; } = new List<Media>();

        public int TotalResults { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnChange += GetSearchResults;
        }

        private async void GetSearchResults()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.Query))
            {
                var data = await SearchService.GetMultiSearchAsync(new MultiSearchParameters { Query = SearchState.Query, Page = 1 });

                TotalResults = data.Total_Results;

                SearchResults = data.Results;
            }

            StateHasChanged();
        }
    }
}
