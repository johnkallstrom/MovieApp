using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class DeleteMovieItemRequest
    {
        [Required(ErrorMessage = "Please provide a valid Movie ID.")]
        public int TmdbId { get; set; }
    }
}
