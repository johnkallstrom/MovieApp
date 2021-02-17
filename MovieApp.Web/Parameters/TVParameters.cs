using System.Collections.Generic;

namespace MovieApp.Web.Parameters
{
    public class TVParameters
    {
        public int Page { get; set; }
        public string SortOrder { get; set; }
        public int FirstAirYear { get; set; }
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
