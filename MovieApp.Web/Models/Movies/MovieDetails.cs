using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Imdb_Id { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
        public string Original_Language { get; set; }
        public string Original_Title { get; set; }
        public string Overview { get; set; }
        public decimal Popularity { get; set; }
        public string Poster_Path { get; set; }
        public string Release_Date { get; set; }
        public int Runtime { get; set; }
        public string Tagline { get; set; }
        public decimal Vote_Average { get; set; }
        public int Vote_Count { get; set; }
    }
}
