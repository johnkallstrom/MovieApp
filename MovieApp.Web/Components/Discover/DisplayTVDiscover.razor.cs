using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components.Discover
{
    public partial class DisplayTVDiscover
    {
        [Parameter]
        public TVShow TVShow { get; set; } = new TVShow();
    }
}
