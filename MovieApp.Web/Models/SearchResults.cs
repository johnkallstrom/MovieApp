using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class SearchResults
    {
        public IEnumerable<Media> Results { get; set; }
        public int Page { get; set; }
        public int Total_Pages { get; set; }
        public int Total_Results { get; set; }
    }
}
