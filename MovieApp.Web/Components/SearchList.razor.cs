using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Components
{
    public partial class SearchList
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public SearchState SearchState { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnQueryChange += RedirectToHome;
            SearchState.OnDataChange += StateHasChanged;
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