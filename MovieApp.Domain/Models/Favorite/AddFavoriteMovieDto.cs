using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class AddFavoriteMovieDto
    {
        [Required(ErrorMessage = "The correct TMDB ID must be provided.")]
        public int TmdbId { get; set; }
        [Required(ErrorMessage = "Please enter the movie title.")]
        public string Title { get; set; }
    }
}
