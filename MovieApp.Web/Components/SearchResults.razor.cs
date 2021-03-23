using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Components
{
    public partial class SearchResults
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public SearchState SearchState { get; set; }

        public void Dispose()
        {
            SearchState.OnQueryChange -= RedirectToHome;
            SearchState.OnResultsChange -= StateHasChanged;
            SearchState.OnTotalResultsChange -= StateHasChanged;
            SearchState.OnFilterChange -= StateHasChanged;
        }

        protected override void OnInitialized()
        {
            SearchState.OnQueryChange += RedirectToHome;
            SearchState.OnResultsChange += StateHasChanged;
            SearchState.OnTotalResultsChange += StateHasChanged;
            SearchState.OnFilterChange += StateHasChanged;
        }

        protected void HandlePageChanged(int selectedPage)
        {
            SearchState.SetPage(selectedPage);
        }

        private void RedirectToHome()
        {
            if (string.IsNullOrEmpty(SearchState.Query) && NavigationManager.Uri == $"{NavigationManager.BaseUri}search")
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
