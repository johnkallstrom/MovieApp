using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components.Movies
{
    public partial class DisplayMovie
    {
        [Parameter]
        public Movie Movie { get; set; }
    }
}
