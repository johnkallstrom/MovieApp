using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Shared
{
    public partial class RecentlyViewed
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        public List<Movie> StoredMovies { get; set; }

        protected async Task ClearLocalStorage() => await LocalStorage.ClearAsync();
    }
}
