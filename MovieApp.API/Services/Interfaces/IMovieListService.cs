using MovieApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Services
{
    public interface IMovieListService
    {
        Task CreateAsync(MovieList movieList);
        Task<IEnumerable<MovieList>> GetAsync(int userId);
    }
}
