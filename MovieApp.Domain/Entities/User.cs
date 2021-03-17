using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            FavoriteMovies = new HashSet<FavoriteMovie>();
            FavoriteTVShows = new HashSet<FavoriteTV>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<FavoriteMovie> FavoriteMovies { get; set; }
        public virtual ICollection<FavoriteTV> FavoriteTVShows { get; set; }
    }
}
