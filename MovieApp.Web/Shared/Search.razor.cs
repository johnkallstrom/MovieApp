using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
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

        public void Dispose()
        {
            SearchState.OnQueryChange -= ResetPage;
            SearchState.OnQueryChange -= GetSearchResults;
            SearchState.OnPageChange -= GetSearchResults;
        }

        protected override void OnInitialized()
        {
            SearchState.OnQueryClear += StateHasChanged;
            SearchState.OnQueryChange += ResetPage;
            SearchState.OnFilterChange += ResetPage;
            SearchState.OnQueryChange += ResetFilter;
            SearchState.OnQueryChange += GetSearchResults;
            SearchState.OnPageChange += GetSearchResults;
            SearchState.OnFilterChange += GetSearchResults;
        }

        private void ResetFilter()
        {
            SearchState.SetFilter(SearchFilterType.All);
        }

        private void ResetPage()
        {
            SearchState.SetPage(SearchState.Page = 1);
        }

        private void NavigateToSearch()
        {
            if (NavigationManager.Uri != $"{NavigationManager.BaseUri}search")
            {
                NavigationManager.NavigateTo("/search");
            }
        }

        private async void GetSearchResults()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.Query))
            {
                SearchResults data;

                switch (SearchState.Filter)
                {
                    case SearchFilterType.All:
                        data = await SearchService.GetMultiSearchAsync(SearchState.Query, SearchState.Page);
                        break;
                    case SearchFilterType.Movies:
                        data = await SearchService.GetMovieSearchAsync(SearchState.Query, SearchState.Page);
                        break;
                    case SearchFilterType.TV:
                        data = await SearchService.GetTVSearchAsync(SearchState.Query, SearchState.Page);
                        break;
                    case SearchFilterType.People:
                        data = await SearchService.GetPeopleSearchAsync(SearchState.Query, SearchState.Page);
                        break;
                    default:
                        data = await SearchService.GetMultiSearchAsync(SearchState.Query, SearchState.Page);
                        break;
                }

                if (data != null)
                {
                    SearchState.SetResults(data.Results);
                    SearchState.SetTotalPages(data.Total_Pages);
                    SearchState.SetTotalResults(data.Total_Results);
                }

                NavigateToSearch();
            }
        }
    }
}