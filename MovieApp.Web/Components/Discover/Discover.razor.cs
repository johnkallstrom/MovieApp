using MovieApp.Web.Models;
using System.Collections.Generic;

namespace MovieApp.Web.Components.Discover
{
    public partial class Discover
    {
        public int Page { get; set; } = 1;
        public IEnumerable<Movie> MovieResults { get; set; } = new List<Movie>();
        public IEnumerable<TVShow> TVResults { get; set; } = new List<TVShow>();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }
}
