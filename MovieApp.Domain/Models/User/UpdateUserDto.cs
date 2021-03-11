using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class UpdateUserDto
    {
        [EmailAddress(ErrorMessage = "The email you entered is invalid.")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
    }
}
