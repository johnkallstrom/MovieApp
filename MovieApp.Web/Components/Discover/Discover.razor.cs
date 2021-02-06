using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using MovieApp.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Components.Discover
{
    public partial class Discover
    {
        [Inject]
        public IDiscoverService DiscoverService { get; set; }

        public bool ShowMovieResults { get; set; } = true;
        public int Page { get; set; } = 1;
        public string SortOrder { get; set; }
        public string SelectedMedia { get; set; } = MediaType.Movie;
        public IEnumerable<Movie> MovieResults { get; set; } = new List<Movie>();
        public IEnumerable<TVShow> TVResults { get; set; } = new List<TVShow>();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        protected async Task HandleClick()
        {
            Page = 1;
            await GetData();
        }

        protected void HandleSortSelection(string selectedSortOrder)
        {
            SortOrder = selectedSortOrder;
        }

        protected void HandleMediaSelection(string selectedMedia)
        {
            SelectedMedia = selectedMedia;
        }

        protected async Task HandlePageChanged(int selectedPage)
        {
            Page = selectedPage;
            await GetData();
        }

        private async Task GetData()
        {
            MovieResults movieData = null;
            TVResults tvData = null;

            switch (SelectedMedia)
            {
                case MediaType.Movie:
                    movieData = await DiscoverService.GetMoviesAsync(new MovieParameters { Page = Page, SortOrder = SortOrder });
                    ShowMovieResults = true;
                    break;
                case MediaType.TV:
                    tvData = await DiscoverService.GetTVAsync(new TVParameters { Page = Page, SortOrder = SortOrder });
                    ShowMovieResults = false;
                    break;
                default:
                    movieData = await DiscoverService.GetMoviesAsync(new MovieParameters { Page = Page, SortOrder = SortOrder });
                    break;
            }

            if (movieData is not null)
            {
                Page = movieData.Page;
                MovieResults = movieData.Results;
                TotalPages = movieData.Total_Pages;
                TotalResults = movieData.Total_Results;
            }

            if (tvData is not null)
            {
                Page = tvData.Page;
                TVResults = tvData.Results;
                TotalPages = tvData.Total_Pages;
                TotalResults = tvData.Total_Results;
            }
        }
    }
}
