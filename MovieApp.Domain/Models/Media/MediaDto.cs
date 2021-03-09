namespace MovieApp.Domain.Models
{
    public class MediaDto
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Type { get; set; }
    }
}
