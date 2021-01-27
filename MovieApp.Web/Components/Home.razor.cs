using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class Home
    {
        [Inject]
        public SearchState SearchState { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISearchService SearchService { get; set; }

        public IEnumerable<Media> Results { get; set; } = new List<Media>();

        public string SearchQuery { get; set; }

        public int CurrentPage { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnChange += GetSearchResults;
        }

        #region Private Methods
        private async void GetSearchResults()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.SearchQuery))
            {
                if (NavigationManager.Uri != NavigationManager.BaseUri)
                {
                    NavigationManager.NavigateTo("/");
                }

                var data = await SearchService.GetMultiSearchAsync(SearchState.SearchQuery);

                if (data != null)
                {
                    SearchQuery = SearchState.SearchQuery;
                    CurrentPage = data.Page;
                    TotalResults = data.Total_Results;
                    TotalPages = data.Total_Pages;
                    Results = data.Results;
                }
            }

            StateHasChanged();
        }

        private async Task SearchChanged(string searchQuery)
        {
            SearchQuery = searchQuery;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var data = await SearchService.GetMultiSearchAsync(searchQuery);

                if (data != null)
                {
                    CurrentPage = data.Page;
                    TotalResults = data.Total_Results;
                    TotalPages = data.Total_Pages;
                    Results = data.Results;
                }
            }
        }
        #endregion
    }
}
