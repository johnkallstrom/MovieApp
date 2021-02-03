using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components.Movies
{
    public partial class DisplayMovie
    {
        //[Inject]
        //public ILocalStorageService LocalStorage { get; set; }

        [Parameter]
        public Movie Movie { get; set; }

        [Parameter]
        public bool ShowTitle { get; set; }

        //protected async Task SaveToLocalStorage()
        //{
        //    string key = $"{Movie.Id} - {Movie.Title}";

        //    var storedMovie = await LocalStorage.GetItemAsync<Movie>(key);

        //    if (storedMovie is null)
        //    {
        //        await LocalStorage.SetItemAsync(key, Movie);
        //    }
        //}
    }
}
