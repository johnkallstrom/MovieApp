using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;
using System;

namespace MovieApp.Web.Components
{
    public partial class Pagination
    {
        [Inject]
        public SearchState SearchState { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnResultsChange += StateHasChanged;
        }
    }
}
