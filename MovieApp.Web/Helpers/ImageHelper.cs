using MovieApp.Web.Models;
using System.Linq;

namespace MovieApp.Web.Helpers
{
    public static class ImageHelper
    {
        public static string GetImageUrl(string filePath, string sizeType, Image imageConfig)
        {
            string url = string.Empty;

            if (!string.IsNullOrEmpty(filePath))
            {
                url = $"{imageConfig.Secure_Base_Url}{imageConfig.Poster_Sizes.FirstOrDefault(s => s.StartsWith(sizeType))}/{filePath}";
            }

            return url;
        }
    }
}
