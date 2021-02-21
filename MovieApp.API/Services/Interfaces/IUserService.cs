using MovieApp.API.Entities;
using System.Collections.Generic;

namespace MovieApp.API.Services
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
        public User GetUser(int userId);
    }
}
