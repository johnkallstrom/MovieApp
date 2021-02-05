using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;

namespace MovieApp.Web.Components.Search
{
    public partial class SearchPagination
    {
        [Inject]
        public SearchState SearchState { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnTotalPagesChange += StateHasChanged;
        }
    }
}
