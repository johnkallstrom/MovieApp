using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class MovieListItem
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Name { get; set; }
        public int MovieListId { get; set; }

        public virtual MovieList MovieList { get; set; }
    }
}
