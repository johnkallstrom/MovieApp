namespace MovieApp.Web.Models
{
    public class PersonDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Adult { get; set; }
        public string Birthday { get; set; }
        public string Deathday { get; set; }
        public string Biography { get; set; }
        public string Imdb_Id { get; set; }
        public string Known_For_Department { get; set; }
        public string Homepage { get; set; }
        public string Profile_Path { get; set; }
        public decimal Popularity { get; set; }
        public string Place_Of_Birth { get; set; }
    }
}
