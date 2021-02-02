using Microsoft.AspNetCore.Components;
using MovieApp.Web.Enums;
using MovieApp.Web.Models;

namespace MovieApp.Web.Components
{
    public partial class DisplayMedia
    {
        [Parameter]
        public Media Media { get; set; }

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
