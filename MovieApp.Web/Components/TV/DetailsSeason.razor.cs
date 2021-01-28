using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.TV
{
    public partial class DetailsSeason
    {
        [Inject]
        public ITVService TVService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Number { get; set; }

        public TVShowDetails TVShowDetails { get; set; } = new TVShowDetails();

        public SeasonDetails SeasonDetails { get; set; } = new SeasonDetails();

        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(Id, out int tvShowId) && int.TryParse(Number, out int seasonNumber))
            {
                SeasonDetails = await TVService.GetTVSeasonDetailsAsync(tvShowId, seasonNumber);
                TVShowDetails = await TVService.GetTVDetailsAsync(tvShowId);
            }
        }
    }
}
