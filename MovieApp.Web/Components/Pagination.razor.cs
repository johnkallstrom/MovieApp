using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;

namespace MovieApp.Web.Components
{
    public partial class Pagination
    {
        [Inject]
        public SearchState SearchState { get; set; }

        [Parameter]
        public int TotalPages { get; set; }
    }
}
