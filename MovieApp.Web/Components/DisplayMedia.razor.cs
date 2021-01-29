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
            string output = "";

            switch (Media.Media_Type)
            {
                case MediaType.Movie:
                    output = "Movie";
                    break;
                case MediaType.TV:
                    output = "TV Show";
                    break;
                case MediaType.Person:
                    output = "Person";
                    break;
            }

            return output;
        }

        protected string GetImagePath()
        {
            string output = "";

            switch (Media.Media_Type)
            {
                case MediaType.Movie:
                    output = Media.Poster_Path;
                    break;
                case MediaType.TV:
                    output = Media.Poster_Path;
                    break;
                case MediaType.Person:
                    output = Media.Profile_Path;
                    break;
            }

            return output;
        }
    }
}
