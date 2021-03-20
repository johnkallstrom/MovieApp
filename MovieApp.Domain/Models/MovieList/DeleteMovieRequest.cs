using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class DeleteMovieRequest
    {
        [Required(ErrorMessage = "Please provide a valid Movie ID.")]
        public int MovieId { get; set; }
    }
}
