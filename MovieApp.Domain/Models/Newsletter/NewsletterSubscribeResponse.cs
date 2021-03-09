using System;

namespace MovieApp.Domain.Models
{
    public class NewsletterSubscribeResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}
