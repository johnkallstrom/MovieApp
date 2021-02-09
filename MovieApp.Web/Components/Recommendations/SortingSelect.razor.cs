using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using System.Collections.Generic;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class SortingSelect
    {
        [Parameter]
        public EventCallback<string> OnSortSelection { get; set; }

        public IEnumerable<SelectOption> MovieSortingOptions { get; set; } = new List<SelectOption>
        {
            new SelectOption("Title Ascending", SortingMovieType.TitleAscending),
            new SelectOption("Title Descending", SortingMovieType.TitleDescending),
            new SelectOption("Popularity Ascending", SortingMovieType.PopularityAscending),
            new SelectOption("Popularity Descending", SortingMovieType.PopularityDescending),
            new SelectOption("Release Ascending", SortingMovieType.ReleaseAscending),
            new SelectOption("Release Descending", SortingMovieType.ReleaseDescending),
            new SelectOption("Rating Ascending", SortingMovieType.RatingAscending),
            new SelectOption("Rating Descending", SortingMovieType.RatingDescending),
        };
    }
}
