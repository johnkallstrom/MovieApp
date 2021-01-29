using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Components
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public SearchState SearchState { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnSearchQueryClear += StateHasChanged;
            SearchState.OnSearchResultsChange += RefreshComponent;
        }

        private void RefreshComponent()
        {
            if (NavigationManager.Uri != NavigationManager.BaseUri)
            {
                NavigationManager.NavigateTo("/");
            }

            StateHasChanged();
        }


        protected void CurrentPageChanged(int currentPage)
        {
            Console.WriteLine("Current Page: " + currentPage);
        }
    }
}
