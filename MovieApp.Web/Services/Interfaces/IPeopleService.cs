using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IPeopleService
    {
        Task<IEnumerable<Movie>> GetPersonMoviesAsync(int personId);
        Task<PersonDetails> GetPersonAsync(int personId);
        Task<IEnumerable<Person>> GetPopularPeopleAsync();
    }
}
