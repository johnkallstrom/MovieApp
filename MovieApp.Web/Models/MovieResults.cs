using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class MovieResults
    {
        public IEnumerable<Movie> Results { get; set; }
        public int Total_Pages { get; set; }
        public int Total_Results { get; set; }
    }
}
