using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class MovieCredits
    {
        public int Id { get; set; }
        public IEnumerable<Person> Cast { get; set; }
    }
}
