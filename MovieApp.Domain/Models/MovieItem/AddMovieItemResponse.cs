namespace MovieApp.Domain.Models
{
    public class AddMovieItemResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public int MovieListId { get; set; }
    }
}
