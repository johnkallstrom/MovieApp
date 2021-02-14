using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface ITVService
    {
        Task<SeasonDetails> GetTVSeasonDetailsAsync(int tvShowId, int seasonNumber);
        Task<IEnumerable<Person>> GetTVCastAsync(int tvShowId);
        Task<TVShowDetails> GetTVDetailsAsync(int tvShowId);
        Task<TVResults> GetOnTheAirTVAsync();
        Task<TVResults> GetOnTheAirTVAsync(int page);
        Task<TVResults> GetPopularTVAsync();
        Task<TVResults> GetPopularTVAsync(int page);
        Task<TVResults> GetTopRatedTVAsync();
        Task<TVResults> GetTopRatedTVAsync(int page);
    }
}
