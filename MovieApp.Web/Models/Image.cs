using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class Image
    {
        public string Base_Url { get; set; }
        public string Secure_Base_Url { get; set; }
        public List<string> Backdrop_Sizes { get; set; }
        public List<string> Logo_Sizes { get; set; }
        public List<string> Poster_Sizes { get; set; }
        public List<string> Profile_Sizes { get; set; }
    }
}
