using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class CreateMovieListRequest
    {
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }
    }
}
