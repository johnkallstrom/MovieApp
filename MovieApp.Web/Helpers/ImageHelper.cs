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
    }
}