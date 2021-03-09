using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class Media
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Type { get; set; }
        public int MediaListId { get; set; }

        public virtual MediaList MediaList { get; set; }
    }
}
