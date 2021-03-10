using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class Movie
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public int ListId { get; set; }

        public virtual List List { get; set; }
    }
}
