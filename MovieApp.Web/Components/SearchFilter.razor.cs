using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;

namespace MovieApp.Web.Components
{
    public partial class SearchFilter
    {
        [Inject]
        public SearchState SearchState { get; set; }
    }
}
