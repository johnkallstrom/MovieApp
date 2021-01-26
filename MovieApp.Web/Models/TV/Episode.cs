using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Still_Path { get; set; }
        public decimal Vote_Average { get; set; }
        public int Vote_Count { get; set; }
        public string Air_Date { get; set; }
        public int Season_Number { get; set; }
        public int Episode_Number { get; set; }
        public IEnumerable<Person> Crew { get; set; } = new List<Person>();
        public IEnumerable<Person> Guest_Stars { get; set; } = new List<Person>();
    }
}
