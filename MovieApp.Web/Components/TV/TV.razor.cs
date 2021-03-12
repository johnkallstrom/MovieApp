using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.TV
{
    public partial class TV
    {
        [Inject]
        public ITVHttpService TVService { get; set; }

        public IEnumerable<TVShowDetails> PopularTVShows { get; set; } = new List<TVShowDetails>();
        public IEnumerable<TVShowDetails> TopRatedTVShows { get; set; } = new List<TVShowDetails>();
        public IEnumerable<TVShowDetails> OnTheAirTVShows { get; set; } = new List<TVShowDetails>();

        protected override async Task OnInitializedAsync()
        {
            var popularTvShows = await TVService.GetPopularTVAsync();
            var topRatedTvShows = await TVService.GetTopRatedTVAsync();
            var onTheAirTvShows = await TVService.GetOnTheAirTVAsync();

            if (popularTvShows is not null && topRatedTvShows is not null && onTheAirTvShows is not null)
            {
                PopularTVShows = popularTvShows.Results;
                TopRatedTVShows = topRatedTvShows.Results;
                OnTheAirTVShows = onTheAirTvShows.Results;
            }
        }
    }
}
