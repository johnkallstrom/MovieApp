using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.State;
using System;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Search
{
    public partial class SearchList : IDisposable
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

        private string DisplayFilter()
        {
            string output = "";

            switch (SearchState.Filter)
            {
                case SearchFilterType.All:
                    output = string.Empty;
                    break;
                case SearchFilterType.Movies:
                    output = "movie";
                    break;
                case SearchFilterType.TV:
                    output = "TV";
                    break;
                case SearchFilterType.People:
                    output = "people";
                    break;
            }

            return output;
        }
    }
}