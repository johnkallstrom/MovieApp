using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class MovieList
    {
        public MovieList()
        {
            Movies = new HashSet<MovieItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MovieItem> Movies { get; set; }
    }
}
