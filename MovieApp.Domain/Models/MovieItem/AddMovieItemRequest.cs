using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class AddMovieItemRequest
    {
        [Required(ErrorMessage = "Please provide a valid Movie ID.")]
        public int TmdbId { get; set; }
        [Required(ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }
    }
}
