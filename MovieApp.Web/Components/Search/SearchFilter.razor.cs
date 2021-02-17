using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.State;
using System.Collections.Generic;

namespace MovieApp.Web.Components.Search
{
    public partial class SearchFilter
    {
        [Inject]
        public SearchState SearchState { get; set; }

        public IEnumerable<SearchFilterType> FilterOptions { get; set; } = new List<SearchFilterType>()
        {
            SearchFilterType.All,
            SearchFilterType.Movies,
            SearchFilterType.TV,
            SearchFilterType.People
        };
    }
}
