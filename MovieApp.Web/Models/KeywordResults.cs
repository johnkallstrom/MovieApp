using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class KeywordResults
    {
        public int Page { get; set; }
        public IEnumerable<Keyword> Results { get; set; } = new List<Keyword>();
    }
}
