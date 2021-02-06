using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;

namespace MovieApp.Web.Components.Discover
{
    public partial class PaginationDiscover
    {
        [Parameter]
        public int Page { get; set; }

        [Parameter]
        public int TotalPages { get; set; }

        [Parameter]
        public EventCallback<int> OnPageChanged { get; set; }
    }
}
