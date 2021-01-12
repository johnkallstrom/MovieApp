using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;

namespace MovieApp.Web.Components.Movies
{
    public partial class Movies
    {
        [Inject]
        public SearchService SearchService { get; set; }

        protected override void OnInitialized()
        {
            SearchService.OnChange += StateHasChanged;
        }
    }
}
