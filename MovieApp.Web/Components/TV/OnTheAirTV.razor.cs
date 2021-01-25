using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.TV
{
    public partial class OnTheAirTV
    {
        [Inject]
        public ITVService TVService { get; set; }

        [Parameter]
        public string HeaderText { get; set; } = "On The Air";

        public IEnumerable<TVShow> TVShows { get; set; } = new List<TVShow>();

        protected override async Task OnInitializedAsync()
        {
            TVShows = await TVService.GetOnTheAirTVAsync();
        }
    }
}
