using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class Media
    {
        public int Id { get; set; }
        public int TheMovieDatabaseId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public int MediaTypeId { get; set; }
        public int ListId { get; set; }

        public virtual List List { get; set; }
        public virtual MediaType MediaType { get; set; }
    }
}
