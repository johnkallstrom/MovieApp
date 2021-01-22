using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class TVShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string First_Air_Date { get; set; }
        public IEnumerable<int> GenreIds { get; set; }
        public string Origin_Counry { get; set; }
        public string Original_Language { get; set; }
        public string Overview { get; set; }
        public string Poster_Path { get; set; }
        public string Backdrop_Path { get; set; }
        public decimal Popularity { get; set; }
        public decimal Vote_Average { get; set; }
        public int Vote_Count { get; set; }
    }
}
