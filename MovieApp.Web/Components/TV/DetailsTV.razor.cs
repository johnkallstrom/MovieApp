using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MovieApp.Web.Models;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.TV
{
    public partial class DetailsTV
    {
        [Inject]
        public ITVHttpService TVService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IConfiguration Config { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        public TVShowDetails TVShow { get; set; } = new TVShowDetails();

        public IEnumerable<Person> Cast { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(Id, out int tvShowId))
            {
                TVShow = await TVService.GetTVDetailsAsync(tvShowId);
                Cast = await TVService.GetTVCastAsync(tvShowId);
            }

            await SetTVShowInLocalStorage();
        }

        private async Task SetTVShowInLocalStorage()
        {
            var tvShow = await LocalStorage.GetItemAsync<TVShowDetails>($"{Config["RecentlyViewed:TVKey"]}{TVShow.Id}");

            if (tvShow is null)
            {
                await LocalStorage.SetItemAsync($"{Config["RecentlyViewed:TVKey"]}{TVShow.Id}", TVShow);
            }
        }
    }
}
