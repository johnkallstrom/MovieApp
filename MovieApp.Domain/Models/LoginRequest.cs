using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a valid password.")]
        public string Password { get; set; }
    }
}
