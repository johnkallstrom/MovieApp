using System;

namespace MovieApp.API.Models
{
    public class RegisterResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
    }
}
