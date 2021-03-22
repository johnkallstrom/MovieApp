namespace MovieApp.Domain.Models
{
    public class MovieItemDto
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public int MovieListId { get; set; }
    }
}
