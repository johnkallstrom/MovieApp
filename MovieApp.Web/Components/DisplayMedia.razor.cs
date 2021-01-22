using Microsoft.AspNetCore.Components;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components
{
    public partial class DisplayMedia
    {
        [Parameter]
        public Media Media { get; set; }

        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public bool ShowTitle { get; set; }

        [Parameter]
        public bool ShowName { get; set; }

        protected string GetMediaUrl()
        {
            string url = string.Empty;

            switch (Type)
            {
                case MediaType.Movie:
                    url = $"/movie/{Media.Id}";
                    break;
                case MediaType.Person:
                    url = $"/person/{Media.Id}";
                    break;
                case MediaType.TV:
                    break;
            }

            return url;
        }

        protected string GetImagePath()
        {
            string path = string.Empty;

            switch (Type)
            {
                case MediaType.Movie:
                    path = $"{Media.Poster_Path}";
                    break;
                case MediaType.Person:
                    path = $"{Media.Profile_Path}";
                    break;
                case MediaType.TV:
                    break;
            }

            return path;
        }
    }
}
