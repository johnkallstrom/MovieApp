using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Domain.Models
{
    public class MovieListItemDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Name { get; set; }
        public int MovieListId { get; set; }
    }
}
