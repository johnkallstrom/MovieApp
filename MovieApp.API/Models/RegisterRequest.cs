using System.ComponentModel.DataAnnotations;

namespace MovieApp.API.Models
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "The passwords you entered does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
