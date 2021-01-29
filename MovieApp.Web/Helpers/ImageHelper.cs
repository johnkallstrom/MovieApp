namespace MovieApp.Web.Helpers
{
    public static class ImageHelper
    {
        public static string GetImageUrl(string imageBaseUrl, string sizeType, string filePath)
        {
            string url = string.Empty;

            if (!string.IsNullOrEmpty(filePath))
            {
                url = $"{imageBaseUrl}{sizeType}/{filePath}";
            }

            return url;
        }
    }
}