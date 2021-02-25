using System;

namespace MovieApp.Domain.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string Token { get; set; }
    }
}
