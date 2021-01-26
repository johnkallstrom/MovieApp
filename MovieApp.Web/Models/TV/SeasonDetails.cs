using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class SeasonDetails
    {
        public int Id { get; set; }
        public string _id { get; set; }
        public int Season_Number { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Poster_Path { get; set; }
        public string Air_Date { get; set; }
        public IEnumerable<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
