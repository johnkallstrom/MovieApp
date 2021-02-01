using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Shared
{
    public partial class Search : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public SearchState SearchState { get; set; }

        [Inject]
        public ISearchService SearchService { get; set; }

        public string Placeholder { get; set; } = "Search...";

        protected override void OnInitialized()
        {
            SearchState.OnQueryClear += StateHasChanged;
            SearchState.OnQueryChange += ResetCurrentPage;
            SearchState.OnQueryChange += GetSearchResults;
            SearchState.OnPageChange += GetSearchResults;
        }

        private void ResetCurrentPage()
        {
            SearchState.SetPage(SearchState.Page = 1);
        }

        protected void NavigateToSearch()
        {
            if (NavigationManager.Uri != $"{NavigationManager.BaseUri}search")
            {
                NavigationManager.NavigateTo("/search");
            }
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

                NavigateToSearch();
            }
        }

        public void Dispose()
        {
            SearchState.OnQueryChange -= ResetCurrentPage;
            SearchState.OnQueryChange -= GetSearchResults;
            SearchState.OnPageChange -= GetSearchResults;
        }
    }
}