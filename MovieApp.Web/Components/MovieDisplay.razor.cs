using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components
{
    public partial class MovieDisplay
    {
        [Parameter]
        public Movie Movie { get; set; }
    }
}
