using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System;
using System.Threading.Tasks;

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
            SearchState.OnQueryChange += ResetPage;
            SearchState.OnFilterChange += ResetPage;
            SearchState.OnQueryChange += ResetFilter;
            SearchState.OnQueryChange += GetSearchResults;
            SearchState.OnPageChange += GetSearchResults;
            SearchState.OnFilterChange += GetSearchResults;
        }

        public void Dispose()
        {
            SearchState.OnQueryChange -= ResetFilter;
            SearchState.OnQueryChange -= ResetPage;
            SearchState.OnFilterChange -= GetSearchResults;
            SearchState.OnQueryChange -= GetSearchResults;
            SearchState.OnPageChange -= GetSearchResults;
        }

        private void NavigateToSearch()
        {
            if (NavigationManager.Uri != $"{NavigationManager.BaseUri}search")
            {
                NavigationManager.NavigateTo("/search");
            }
        }

        private void GetSearchResults()
        {
            Task.Run(async () =>
            {
                if (!string.IsNullOrWhiteSpace(SearchState.Query))
                {
                    MediaResults data;

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

                    if (data is not null)
                    {
                        SearchState.SetResults(data.Results);
                        SearchState.SetTotalPages(data.Total_Pages);
                        SearchState.SetTotalResults(data.Total_Results);
                    }

                    NavigateToSearch();
                }
            }).Wait(new TimeSpan(4000000));
        }

        private void ResetFilter() => SearchState.ResetFilter();

        private void ResetPage() => SearchState.ResetPage();
    }
}