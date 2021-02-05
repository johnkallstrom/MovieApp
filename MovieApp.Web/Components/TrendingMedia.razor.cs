using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class TrendingMedia
    {
        [Inject]
        public ITrendingService TrendingService { get; set; }

        public IEnumerable<Media> Items { get; set; } = new List<Media>();

        public string SelectedMediaType { get; set; } = MediaType.All;
        public string SelectedTimeWindow { get; set; } = "Day";

        public IEnumerable<string> TimeWindowOptions { get; set; } = new List<string>
        {
            "Day",
            "Week"
        };

        public IEnumerable<string> MediaTypeOptions { get; set; } = new List<string>
        {
            MediaType.All,
            MediaType.Movie,
            MediaType.TV,
            MediaType.Person
        };

        protected override async Task OnInitializedAsync()
        {
            Items = await TrendingService.GetTrendingItemsAsync(SelectedMediaType, SelectedTimeWindow.ToLower());
        }

        protected async Task HandleTimeWindowSelection(ChangeEventArgs e)
        {
            SelectedTimeWindow = e.Value.ToString();
            Items = await TrendingService.GetTrendingItemsAsync(SelectedMediaType, SelectedTimeWindow.ToLower());
        }

        protected async Task HandleMediaTypeSelection(ChangeEventArgs e)
        {
            SelectedMediaType = e.Value.ToString();
            Items = await TrendingService.GetTrendingItemsAsync(SelectedMediaType, SelectedTimeWindow.ToLower());
        }
    }
}
