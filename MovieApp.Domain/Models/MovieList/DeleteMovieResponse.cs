﻿namespace MovieApp.Domain.Models
{
    public class DeleteMovieResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int MovieId { get; set; }
        public string Name { get; set; }
        public int MovieListId { get; set; }
    }
}
