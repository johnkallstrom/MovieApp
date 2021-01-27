using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.State;
using System.Collections.Generic;

namespace MovieApp.Web.Components
{
    public partial class MediaList
    {
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

        protected override void OnInitialized()
        {
            SearchState.OnChange += StateHasChanged;
        }
    }
}
