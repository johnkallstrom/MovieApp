using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;

namespace MovieApp.Web.Components
{
    public partial class MediaTable
    {
        [Inject]
        public SearchState SearchState { get; set; }

        protected override void OnInitialized()
        {
            SearchState.OnDataChange += StateHasChanged;
        }
    }
}
