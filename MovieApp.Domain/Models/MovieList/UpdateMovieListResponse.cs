using System;

namespace MovieApp.Domain.Models
{
    public class UpdateMovieListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}
