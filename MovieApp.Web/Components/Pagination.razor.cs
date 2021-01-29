using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Components
{
    public partial class Pagination : IDisposable
    {
        [Inject]
        public SearchState SearchState { get; set; }

        [Parameter]
        public int TotalPages { get; set; }

        public void Dispose()
        {
            SearchState.OnSearchQueryChange -= ResetCurrentPage;
        }

        protected override void OnInitialized()
        {
            SearchState.OnSearchQueryChange += ResetCurrentPage;
        }

        private void ResetCurrentPage()
        {
            SearchState.SetCurrentPage(SearchState.CurrentPage = 1);
        }
    }
}
