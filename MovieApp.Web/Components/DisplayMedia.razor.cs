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
    }
}
