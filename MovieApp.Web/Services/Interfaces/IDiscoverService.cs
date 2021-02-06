using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IDiscoverService
    {
        Task<MovieResults> GetMoviesAsync(MovieParameters parameters);
        Task<TVResults> GetTVAsync(TVParameters parameters);
    }
}
