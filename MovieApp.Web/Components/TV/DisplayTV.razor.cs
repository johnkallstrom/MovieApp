using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components.TV
{
    public partial class DisplayTV
    {
        [Parameter]
        public TVShow TVShow { get; set; }
    }
}
