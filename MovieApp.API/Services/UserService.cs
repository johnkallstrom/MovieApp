using MovieApp.API.Data;
using MovieApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly MovieAppDatabaseContext _context;

        public UserService(MovieAppDatabaseContext context)
        {
            _context = context;
        }

        public User GetUser(int userId) => _context.Users.FirstOrDefault(u => u.Id == userId);

        public IEnumerable<User> GetUsers()
        {
            var users = _context.Users;

            if (users is null) throw new ArgumentNullException(nameof(users));

            return users;
        }
    }
}
