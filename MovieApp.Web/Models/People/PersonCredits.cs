using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class PersonCredits
    {
        public int Id { get; set; }
        public IEnumerable<Movie> Cast { get; set; }
    }
}
