using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IGenreHttpService
    {
        Task<IEnumerable<Genre>> GetMovieGenresAsync();
        Task<IEnumerable<Genre>> GetTVGenresAsync();
    }
}
