using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Components
{
    public partial class RecentlyViewed
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();

        public List<TVShowDetails> TVShows { get; set; } = new List<TVShowDetails>();

        public string Message { get; set; }
        public bool DisplayMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var keys = await GetRecentlyViewedKeys();

            if (keys is not null && keys.Count() is not 0)
            {
                foreach (var key in keys)
                {
                    if (key.Contains("movie"))
                    {
                        var movie = await LocalStorage.GetItemAsync<Movie>(key);
                        Movies.Add(movie);
                    }

                    if (key.Contains("tv"))
                    {
                        var tvShow = await LocalStorage.GetItemAsync<TVShowDetails>(key);
                        TVShows.Add(tvShow);
                    }
                }
            }

            if (Movies.Count() is 0 && TVShows.Count() is 0)
            {
                DisplayMessage = true;
                Message = "There is no recently viewed items.";
            }
        }

        protected async Task HandleClearBtnClick()
        {
            var keys = await GetRecentlyViewedKeys();

            if (keys is not null && keys.Count() is not 0)
            {
                foreach (var key in keys)
                {
                    await LocalStorage.RemoveItemAsync(key);
                }

                Movies.Clear();
                TVShows.Clear();

                DisplayMessage = true;
                Message = "There is no recently viewed items.";

                StateHasChanged();
            }
        }

        private async Task<List<string>> GetRecentlyViewedKeys()
        {
            var recentlyViewedKeys = new List<string>();
            int storageLength = await LocalStorage.LengthAsync();

            if (storageLength >= 1)
            {
                for (int index = 0; index < storageLength; index++)
                {
                    string key = await LocalStorage.KeyAsync(index);
                    if (key.StartsWith("rv"))
                    {
                        recentlyViewedKeys.Add(key);
                    }
                }
            }
            
            return recentlyViewedKeys;
        }
    }
}
