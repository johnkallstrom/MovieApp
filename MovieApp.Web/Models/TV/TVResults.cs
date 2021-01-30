using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class TVResults
    {
        public int Page { get; set; }
        public IEnumerable<TVShow> Results { get; set; }
        public int Total_Pages { get; set; }
        public int Total_Results { get; set; }
    }
}
