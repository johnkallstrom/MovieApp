using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Domain.Models
{
    public class FavoriteMovieDto
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
    }
}
