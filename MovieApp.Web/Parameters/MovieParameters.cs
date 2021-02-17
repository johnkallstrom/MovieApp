using System.Collections.Generic;

namespace MovieApp.Web.Parameters
{
    public class MovieParameters
    {
        public int Page { get; set; }
        public string SortOrder { get; set; }
        public int ReleaseYear { get; set; }
        public List<int> GenreIds { get; set; } = new List<int>();
        public List<int> ActorIds { get; set; } = new List<int>();
        public List<int> KeywordIds { get; set; } = new List<int>();
    }
}
