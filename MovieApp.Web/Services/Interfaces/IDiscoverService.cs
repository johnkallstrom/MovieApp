using MovieApp.Web.Models;
using MovieApp.Web.Parameters;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface IDiscoverService
    {
        Task<MovieResults> GetMoviesAsync(DiscoverMovieParameters parameters);
        Task<TVResults> GetTVAsync(DiscoverTVParameters parameters);
    }
}
