using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components.Discover
{
    public partial class DisplayMovieDiscover
    {
        [Parameter]
        public Movie Movie { get; set; } = new Movie();
    }
}
