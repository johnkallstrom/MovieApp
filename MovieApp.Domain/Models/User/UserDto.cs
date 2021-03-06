﻿using System;
using System.Collections.Generic;

namespace MovieApp.Domain.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public IEnumerable<MovieListDto> MovieLists { get; set; } = new List<MovieListDto>();
    }
}
