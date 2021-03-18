using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class AddMovieRequest
    {
        [Required(ErrorMessage = "Please provide a valid Movie ID.")]
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
    }
}
