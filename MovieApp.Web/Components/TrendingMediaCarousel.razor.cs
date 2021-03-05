using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class TrendingMediaCarousel
    {
        [Inject]
        public IConfiguration Config { get; set; }

        [Inject]
        public ITrendingService TrendingService { get; set; }

        public IEnumerable<Media> Results { get; set; } = new List<Media>();

        public string PlaceholderImageUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PlaceholderImageUrl = ImageHelper.GetPlaceholderImageUrl(new ImageSettings(Config["TMDB:PlaceholderImageBaseUrl"], 800, 400));

            var data = await TrendingService.GetTrendingItemsAsync("all", "week");

            if (data is not null)
            {
                Results = data;
            }
        }
    }
}
