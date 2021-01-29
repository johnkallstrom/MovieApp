using MovieApp.Web.Models;
using System.Threading.Tasks;

namespace MovieApp.Web.Services
{
    public interface ISearchService
    {
        Task<SearchResults> GetMultiSearchAsync(string query, int page);
    }
}
