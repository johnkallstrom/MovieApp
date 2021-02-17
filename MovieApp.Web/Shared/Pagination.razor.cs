using Microsoft.AspNetCore.Components;

namespace MovieApp.Web.Shared
{
    public partial class Pagination
    {
        [Parameter]
        public int Page { get; set; }

        [Parameter]
        public int TotalPages { get; set; }

        [Parameter]
        public EventCallback<int> OnPageChanged { get; set; }
    }
}
