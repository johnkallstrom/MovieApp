﻿using Microsoft.AspNetCore.Components;
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
            SearchState.OnQueryClear += StateHasChanged;
            SearchState.OnQueryChange += GetSearchResults;
            SearchState.OnPageChange += GetSearchResults;
        }

        protected async void GetSearchResults()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.Query))
            {
                var data = await SearchService.GetMultiSearchAsync(SearchState.Query, SearchState.Page);

                if (data != null)
                {
                    SearchState.SetData(data);
                }
            }
        }

        public void Dispose()
        {
            SearchState.OnQueryChange -= GetSearchResults;
            SearchState.OnPageChange -= GetSearchResults;
        }
    }
}