using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class SortingSelect
    {
        [Parameter]
        public bool ShowMovieOptions { get; set; }

        [Parameter]
        public bool ShowTVOptions { get; set; }

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

        public IEnumerable<SelectOption> TVSortingOptions { get; set; } = new List<SelectOption>
        {
            new SelectOption("Popularity Ascending", SortingTVType.PopularityAscending),
            new SelectOption("Popularity Descending", SortingTVType.PopularityDescending),
            new SelectOption("First Air Date Ascending", SortingTVType.FirstAirDateAscending),
            new SelectOption("First Air Date Descending", SortingTVType.FirstAirDateDescending),
            new SelectOption("Rating Ascending", SortingTVType.RatingAscending),
            new SelectOption("Rating Descending", SortingTVType.RatingDescending),
        };
    }
}
