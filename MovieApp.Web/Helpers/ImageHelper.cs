using MovieApp.Web.Enums;
using MovieApp.Web.Models;

namespace MovieApp.Web.Helpers
{
    public static class ImageHelper
    {
        private const string PlaceholderBackgroundColor = "adadad";
        private const string PlaceholderTextColor = "ffffff";

        public static string GetImageUrl(ImageSettings settings)
        {
            string url;

            url = $"{settings.BaseUrl}{settings.SizeType}/{settings.FilePath}";

            return url;
        }

        public static string GetPlaceholderImageUrl(ImageSettings settings)
        {
            string url;

            url = $"{settings.BaseUrl}{settings.Width}x{settings.Height}/{PlaceholderBackgroundColor}/{PlaceholderTextColor}?text=No+Image";

            return url;
        }

        public static string GetMediaImagePath(Media media)
        {
            string path = "";

            switch (media.Media_Type)
            {
                case MediaType.Movie:
                    path = media.Poster_Path;
                    break;
                case MediaType.TV:
                    path = media.Poster_Path;
                    break;
                case MediaType.Person:
                    path = media.Profile_Path;
                    break;
            }

            return path;
        }
    }
}