using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;
using MovieApp.Web.State;

namespace MovieApp.Web.Shared
{
    public partial class Search
    {
        [Inject]
        public SearchState SearchState { get; set; }

        [Inject]
        public ISearchService SearchService { get; set; }

        public string Placeholder { get; set; } = "Search...";

        protected override void OnInitialized()
        {
            SearchState.OnSearchQueryClear += StateHasChanged;
            SearchState.OnSearchQueryChange += GetSearchResults;
            SearchState.OnCurrentPageChange += GetSearchResults;
        }

        protected async void GetSearchResults()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.SearchQuery))
            {
                var data = await SearchService.GetMultiSearchAsync(SearchState.SearchQuery, SearchState.CurrentPage);

                if (data != null)
                {
                    SearchState.SetSearchResults(data);
                }
            }
        }
    }
}