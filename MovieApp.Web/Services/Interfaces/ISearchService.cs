using MovieApp.Web.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface ISearchService
    {
        Task<SearchResults> GetPeopleSearchAsync(string query, int page);
        Task<SearchResults> GetTVSearchAsync(string query, int page);
        Task<SearchResults> GetMovieSearchAsync(string query, int page);
        Task<SearchResults> GetMultiSearchAsync(string query, int page);
    }
}
