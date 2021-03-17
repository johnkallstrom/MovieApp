using Blazored.LocalStorage;
using Blazored.Toast.Services;
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
        public IToastService ToastService { get; set; }

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

        protected void HandleAddToFavBtnClick()
        {
            ToastService.ShowSuccess($"{TVShow.Name} has been added to your favorites list.");
        }

        protected void HandleDeleteFromFavBtnClick()
        {
            ToastService.ShowError($"{TVShow.Name} has been deleted from your favorites list.");
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
