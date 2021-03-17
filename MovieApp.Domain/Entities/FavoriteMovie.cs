using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class FavoriteMovie
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
