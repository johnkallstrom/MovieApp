using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System.Collections.Generic;

namespace MovieApp.Web.Components
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISearchService SearchService { get; set; }

        [Inject]
        public SearchState SearchState { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<Media> Results { get; set; } = new List<Media>();

        protected override void OnInitialized()
        {
            SearchState.OnChange += ResetCurrentPage;
            SearchState.OnChange += GetSearchResults;
        }

        protected void ResetCurrentPage() => CurrentPage = 1;

        protected void CurrentPageChanged(int currentPage)
        {
            CurrentPage = currentPage;
            GetSearchResults();
        }

        private async void GetSearchResults()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.SearchQuery))
            {
                var data = await SearchService.GetMultiSearchAsync(SearchState.SearchQuery, CurrentPage);

                if (data != null)
                {
                    CurrentPage = data.Page;
                    TotalResults = data.Total_Results;
                    TotalPages = data.Total_Pages;
                    Results = data.Results;
                }

                if (NavigationManager.Uri != NavigationManager.BaseUri)
                {
                    NavigationManager.NavigateTo("/");
                }
            }

            StateHasChanged();
        }
    }
}
