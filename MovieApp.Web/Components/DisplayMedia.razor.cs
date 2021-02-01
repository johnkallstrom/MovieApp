using Microsoft.AspNetCore.Components;
using MovieApp.Web.Helpers;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components
{
    public partial class DisplayMedia
    {
        [Parameter]
        public Media Media { get; set; }

        protected string DisplayMediaType()
        {
            string type = "";

            switch (Media.Media_Type)
            {
                case MediaType.Movie:
                    type = "Movie";
                    break;
                case MediaType.TV:
                    type = "TV Show";
                    break;
                case MediaType.Person:
                    type = "Person";
                    break;
            }

            return type;
        }

        protected string GetImagePath()
        {
            string path = "";

            switch (Media.Media_Type)
            {
                case MediaType.Movie:
                    path = Media.Poster_Path;
                    break;
                case MediaType.TV:
                    path = Media.Poster_Path;
                    break;
                case MediaType.Person:
                    path = Media.Profile_Path;
                    break;
            }

            return path;
        }
    }
}
