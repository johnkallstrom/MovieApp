using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class TrendingMediaCarousel
    {
        [Inject]
        public ITrendingService TrendingService { get; set; }

        public IEnumerable<Media> Results { get; set; } = new List<Media>();

        protected override async Task OnInitializedAsync()
        {
            var data = await TrendingService.GetTrendingItemsAsync("all", "week");

            if (data is not null)
            {
                Results = data;
            }
        }
    }
}
