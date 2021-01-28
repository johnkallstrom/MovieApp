using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using MovieApp.Web.State;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class MediaTable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISearchService SearchService { get; set; }

        [Inject]
        public SearchState SearchState { get; set; }

        [Parameter]
        public string SearchQuery { get; set; }

        [Parameter]
        public int CurrentPage { get; set; }

        [Parameter]
        public int TotalResults { get; set; }

        [Parameter]
        public int TotalPages { get; set; }

        [Parameter]
        public IEnumerable<Media> Results { get; set; } = new List<Media>();

        [Parameter]
        public EventCallback<int> OnCurrentPageChanged { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnChange += StateHasChanged;
        }

        protected async Task HandlePrevBtn()
        {
            CurrentPage--;
            await OnCurrentPageChanged.InvokeAsync(CurrentPage);
        }

        protected async Task HandleNextBtn()
        {
            CurrentPage++;
            await OnCurrentPageChanged.InvokeAsync(CurrentPage);
        }
    }
}
