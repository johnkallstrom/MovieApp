using Microsoft.AspNetCore.Components;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components
{
    public partial class DisplayMedia
    {
        [Parameter]
        public Media Media { get; set; }

        protected string GetMediaUrl()
        {
            string url = string.Empty;

            switch (Media.Media_Type)
            {
                case MediaType.Movie:
                    url = $"/movie/{Media.Id}";
                    break;
                case MediaType.TV:
                    url = $"/tv/{Media.Id}";
                    break;
                case MediaType.Person:
                    url = $"/person/{Media.Id}";
                    break;
            }

            return url;
        }

        protected string GetImageUrl()
        {
            string url = string.Empty;

            switch (Media.Media_Type)
            {
                case MediaType.Movie:
                    url = $"{Media.Poster_Path}";
                    break;
                case MediaType.TV:
                    url = $"{Media.Poster_Path}";
                    break;
                case MediaType.Person:
                    url = $"{Media.Profile_Path}";
                    break;
            }

            return url;
        }
    }
}
