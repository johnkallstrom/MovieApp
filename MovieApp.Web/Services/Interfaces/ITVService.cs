using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface ITVService
    {
        Task<IEnumerable<TVShow>> GetOnTheAirTVAsync();
        Task<IEnumerable<TVShow>> GetPopularTVAsync();
        Task<IEnumerable<TVShow>> GetTopRatedTVAsync();
    }
}
