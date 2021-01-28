using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components.TV
{
    public partial class DisplayEpisode
    {
        [Parameter]
        public Episode Episode { get; set; }
    }
}
