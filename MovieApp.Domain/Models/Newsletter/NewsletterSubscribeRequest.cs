using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Models
{
    public class NewsletterSubscribeRequest
    {
        [Required(ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }
    }
}
