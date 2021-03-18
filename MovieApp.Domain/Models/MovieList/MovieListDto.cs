using System;
using System.Collections.Generic;

namespace MovieApp.Domain.Models
{
    public class MovieListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        public IEnumerable<MovieListItemDto> Items { get; set; }
    }
}
