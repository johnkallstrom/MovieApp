using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class MediaType
    {
        public MediaType()
        {
            Media = new HashSet<Media>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Media> Media { get; set; }
    }
}
