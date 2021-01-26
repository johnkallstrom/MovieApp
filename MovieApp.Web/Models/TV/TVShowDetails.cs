using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class TVShowDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Original_Language { get; set; }
        public string Original_Name { get; set; }
        public string First_Air_Date { get; set; }
        public IEnumerable<string> Origin_Country { get; set; } = new List<string>();
        public decimal Popularity { get; set; }
        public string Poster_Path { get; set; }
        public string Backdrop_Path { get; set; }
        public int Number_Of_Seasons { get; set; }
        public int Number_Of_Episodes { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Type { get; set; }
        public decimal Vote_Average { get; set; }
        public int Vote_Count { get; set; }
        public IEnumerable<Person> Created_By { get; set; } = new List<Person>();
        public IEnumerable<Season> Seasons { get; set; } = new List<Season>();
        public IEnumerable<ProductionCompany> Production_Companies { get; set; } = new List<ProductionCompany>();
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
        public IEnumerable<Network> Networks { get; set; } = new List<Network>();
    }
}
