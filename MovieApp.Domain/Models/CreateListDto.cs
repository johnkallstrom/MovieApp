using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class CreateListDto
    {
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter a created date.")]
        public DateTime Created { get; set; }
    }
}
