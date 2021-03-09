using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            MediaLists = new HashSet<MediaList>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<MediaList> MediaLists { get; set; }
    }
}
