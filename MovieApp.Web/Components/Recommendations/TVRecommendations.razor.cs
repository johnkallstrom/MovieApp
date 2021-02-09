using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;

namespace MovieApp.Web.Components.Recommendations
{
    public partial class TVRecommendations
    {
        [Inject]
        public IDiscoverService DiscoverService { get; set; }

        public int Page { get; set; } = 1;
        public string SortOrder { get; set; }
        public List<Genre> SelectedGenres { get; set; } = new List<Genre>();
        public IEnumerable<TVShow> Results { get; set; } = new List<TVShow>();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }
}
