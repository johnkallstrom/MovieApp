using System;

namespace MovieApp.Domain.Models
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
    }
}
