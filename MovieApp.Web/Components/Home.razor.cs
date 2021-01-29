using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Components
{
    public partial class Home : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public SearchState SearchState { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnSearchQueryChange += RefreshComponent;
            SearchState.OnSearchQueryClear += StateHasChanged;
            SearchState.OnSearchResultsChange += CheckLocationUri;
        }

        private void RefreshComponent()
        {
            if (string.IsNullOrWhiteSpace(SearchState.SearchQuery))
            {
                StateHasChanged();
            }
        }

        private void CheckLocationUri()
        {
            if (NavigationManager.Uri != NavigationManager.BaseUri)
            {
                NavigationManager.NavigateTo("/");
            }

            StateHasChanged();
        }

        public void Dispose()
        {
            SearchState.OnSearchQueryChange -= RefreshComponent;
            SearchState.OnSearchQueryClear -= StateHasChanged;
        }
    }
}
