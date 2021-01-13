using Microsoft.AspNetCore.Components;
using MovieApp.Web.Services;

namespace MovieApp.Web.Shared
{
    public partial class Search
    {
        [Inject]
        public SearchService SearchService { get; set; }
    }
}
