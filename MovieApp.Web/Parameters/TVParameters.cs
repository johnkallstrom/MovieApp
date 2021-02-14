using System.Collections.Generic;

namespace MovieApp.Web.Parameters
{
    public class TVParameters
    {
        public int Page { get; set; }
        public string SortOrder { get; set; }
        public int GenreId { get; set; }
        public int FirstAirYear { get; set; }
    }
}
