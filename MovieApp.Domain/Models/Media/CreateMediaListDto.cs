using MovieApp.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class CreateMediaListDto
    {
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter a created date.")]
        public DateTime Created { get; set; }
        [Required]
        [EnsureOneElement(ErrorMessage = "The list requires atleast one element.")]
        public IEnumerable<MediaDto> Media { get; set; }
    }
}
