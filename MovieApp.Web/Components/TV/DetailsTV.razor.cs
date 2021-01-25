using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;

namespace MovieApp.Web.Components.TV
{
    public partial class DetailsTV
    {
        [Inject]
        public ITVService TVService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public TVShowDetails TVShow { get; set; }
    }
}
