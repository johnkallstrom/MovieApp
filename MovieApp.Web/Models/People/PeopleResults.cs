using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class PeopleResults
    {
        public int Page { get; set; }
        public IEnumerable<Person> Results { get; set; }
        public int Total_Pages { get; set; }
        public int Total_Results { get; set; }
    }
}
