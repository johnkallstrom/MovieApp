using Microsoft.AspNetCore.Components;
using MovieApp.Web.State;

namespace MovieApp.Web.Shared
{
    public partial class Search
    {
        [Inject]
        public SearchState SearchState { get; set; }
    }
}