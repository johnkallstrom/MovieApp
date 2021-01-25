﻿using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.TV
{
    public partial class TopRatedTV
    {
        [Inject]
        public ITVService TVService { get; set; }

        [Parameter]
        public string HeaderText { get; set; } = "Top Rated";

        public IEnumerable<TVShow> TVShows { get; set; } = new List<TVShow>();

        protected override async Task OnInitializedAsync()
        {
            TVShows = await TVService.GetTopRatedTVAsync();
        }
    }
}