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
            SearchState.OnQueryChange += RefreshComponent;
            SearchState.OnDataChange += CheckCurrentLocation;
            SearchState.OnQueryClear += StateHasChanged;
        }

        private void RefreshComponent()
        {
            if (string.IsNullOrWhiteSpace(SearchState.Query))
            {
                StateHasChanged();
            }
        }

        private void CheckCurrentLocation()
        {
            if (NavigationManager.Uri != NavigationManager.BaseUri)
            {
                NavigationManager.NavigateTo("/");
            }

            StateHasChanged();
        }

        public void Dispose()
        {
            SearchState.OnQueryChange -= RefreshComponent;
            SearchState.OnQueryClear -= StateHasChanged;
        }
    }
}
