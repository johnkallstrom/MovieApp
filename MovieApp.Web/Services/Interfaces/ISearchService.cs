using MovieApp.Web.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface ISearchService
    {
        Task<MediaResults> GetPeopleSearchAsync(string query, int page);
        Task<MediaResults> GetTVSearchAsync(string query, int page);
        Task<MediaResults> GetMovieSearchAsync(string query, int page);
        Task<MediaResults> GetMultiSearchAsync(string query, int page);
    }
}
