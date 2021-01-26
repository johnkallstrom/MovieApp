using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class TVCredits
    {
        public int Id { get; set; }
        public IEnumerable<Person> Cast { get; set; }
    }
}
