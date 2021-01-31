using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Components
{
    public partial class Pagination : IDisposable
    {
        [Inject]
        public SearchState SearchState { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnDataChange += StateHasChanged;
            SearchState.OnQueryChange += ResetCurrentPage;
        }

        private void ResetCurrentPage()
        {
            SearchState.SetPage(SearchState.Page = 1);
        }

        public void Dispose()
        {
            SearchState.OnQueryChange -= ResetCurrentPage;
        }
    }
}
