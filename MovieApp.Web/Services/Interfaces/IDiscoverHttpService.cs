using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IDiscoverHttpService
    {
        Task<MovieResults> GetMoviesAsync(MovieParameters parameters);
        Task<TVResults> GetTVAsync(TVParameters parameters);
    }
}
