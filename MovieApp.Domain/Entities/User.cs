﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApp.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Lists = new HashSet<List>();
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

        public virtual ICollection<List> Lists { get; set; }
    }
}
