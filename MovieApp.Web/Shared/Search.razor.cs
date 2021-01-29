using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System;
using System.Collections.Generic;
using System.Threading;

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
        }

        protected async void GetSearchResults()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.SearchQuery))
            {
                var data = await SearchService.GetMultiSearchAsync(SearchState.SearchQuery, 1);

                if (data != null)
                {
                    SearchState.SetSearchResults(data);
                }
            }
        }
    }
}