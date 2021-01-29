﻿using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class ApiConfiguration
    {
        public ImageConfiguration Images { get; set; }

        public IEnumerable<string> Change_Keys { get; set; }
    }

    public class ImageConfiguration
    {
        public string Base_Url { get; set; }
        public string Secure_Base_Url { get; set; }
        public List<string> Backdrop_Sizes { get; set; }
        public List<string> Logo_Sizes { get; set; }
        public List<string> Poster_Sizes { get; set; }
        public List<string> Profile_Sizes { get; set; }
    }
}