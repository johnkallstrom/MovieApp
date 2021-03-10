namespace MovieApp.Domain.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public int MovieListId { get; set; }
    }
}