using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class MovieItem
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public int MovieListId { get; set; }

        public virtual MovieList MovieList { get; set; }
    }
}
