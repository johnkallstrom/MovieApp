using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class TrendingMedia
    {
        [Inject]
        public ITrendingService TrendingService { get; set; }

        public IEnumerable<Media> Items { get; set; } = new List<Media>();

        protected override async Task OnInitializedAsync()
        {
            Items = await TrendingService.GetTrendingItemsAsync(MediaType.All, TimeWindowType.Week);
        }
    }
}
