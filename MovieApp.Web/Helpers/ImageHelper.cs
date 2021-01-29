using MovieApp.Web.Models;
using System.Linq;

namespace MovieApp.Web.Helpers
{
    public static class ImageHelper
    {
        public static string GetImageUrl(string filePath, string sizeType, ApiConfiguration config)
        {
            string url = string.Empty;

            if (!string.IsNullOrEmpty(filePath))
            {
                url = $"{config.Images.Secure_Base_Url}{config.Images.Poster_Sizes.FirstOrDefault(s => s.StartsWith(sizeType))}/{filePath}";
            }

            return url;
        }
    }
}