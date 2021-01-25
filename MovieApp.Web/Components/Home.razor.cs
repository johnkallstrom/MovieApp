using Microsoft.AspNetCore.Components;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Web.Components
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public SearchState SearchState { get; set; }

        [Inject]
        public ISearchService SearchService { get; set; }

        public IEnumerable<Media> Results { get; set; } = new List<Media>();

        public int TotalResults { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; } = 1;

        protected override void OnInitialized()
        {
            SearchState.OnChange += GetSearchResults;
        }

        protected void HandlePrevButton()
        {
            CurrentPage--;
            GetSearchResults();
        }

        protected void HandlePageButton(int page)
        {
            CurrentPage = page;
            GetSearchResults();
        }

        protected void HandleNextButton()
        {
            CurrentPage++;
            GetSearchResults();
        }

        private async void GetSearchResults()
        {
            if (!string.IsNullOrWhiteSpace(SearchState.Query))
            {
                Console.WriteLine("Uri: " + NavigationManager.Uri);
                Console.WriteLine("BaseUri: " + NavigationManager.BaseUri);

                if (NavigationManager.Uri != NavigationManager.BaseUri)
                {
                    NavigationManager.NavigateTo("/");
                }

                var data = await SearchService.GetMultiSearchAsync(new SearchParameters { Query = SearchState.Query, Page = CurrentPage });

                Results = data.Results;
                TotalResults = data.Total_Results;
                TotalPages = data.Total_Pages;

                StateHasChanged();
            }
            else
            {
                StateHasChanged();
            }
        }
    }
}
