using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Shared
{
    public partial class Search : IDisposable
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

        public void Dispose()
        {
            SearchState.OnSearchQueryChange -= GetSearchResults;
            SearchState.OnCurrentPageChange -= GetSearchResults;
        }
    }
}