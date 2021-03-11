using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Please enter a username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "The email you entered is invalid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a valid password.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter a valid confirm password.")]
        [Compare(nameof(Password), ErrorMessage = "The passwords you entered does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
