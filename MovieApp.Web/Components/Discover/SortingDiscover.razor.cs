using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using System.Collections.Generic;

namespace MovieApp.Web.Components.Discover
{
    public partial class SortingDiscover
    {
        [Parameter]
        public EventCallback<string> OnSortSelection { get; set; }

        public IEnumerable<SelectOption> SortingOptions { get; set; } = new List<SelectOption>
        {
            new SelectOption("Title Ascending", SortingType.TitleAscending),
            new SelectOption("Title Descending", SortingType.TitleDescending),
            new SelectOption("Popularity Ascending", SortingType.PopularityAscending),
            new SelectOption("Popularity Descending", SortingType.PopularityDescending),
            new SelectOption("Release Ascending", SortingType.ReleaseAscending),
            new SelectOption("Release Descending", SortingType.ReleaseDescending),
            new SelectOption("Rating Ascending", SortingType.RatingAscending),
            new SelectOption("Rating Descending", SortingType.RatingDescending), 
        };
    }
}
