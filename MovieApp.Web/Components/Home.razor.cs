using Microsoft.AspNetCore.Components;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Web.Components
{
    public partial class Home
    {
        [Inject]
        public SearchState SearchState { get; set; }

        [Inject]
        public ISearchService SearchService { get; set; }

        public IEnumerable<Media> SearchResults { get; set; } = new List<Media>();

        protected override void OnInitialized()
        {
            SearchState.OnChange += GetSearchResults;
        }

        private async void GetSearchResults()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.Query))
            {
                SearchResults = await SearchService.GetMultiSearchAsync(new SearchParameters { Query = SearchState.Query, Page = 1 });
                StateHasChanged();
            }
            else
            {
                StateHasChanged();
            }
        }
    }
}
